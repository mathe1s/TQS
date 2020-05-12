using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using TQS.Consolos;
using TQS.Drawing2D;
using TQS.Model;
using System.IO;
using System.Windows.Input;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace TQS.ViewModel
{
    public class RINTCONS_VM : VMObject
    {
        #region PropFull

        private string selectedBuildingName;

        public string SelectedBuildingName
        {
            get { return selectedBuildingName; }
            set { selectedBuildingName = value; OnPropertyChanged(nameof(SelectedBuildingName)); }
        }


        //Lista para armazenar os consolos
        private ObservableCollection<ConsoleModel> listConsole;
        public ObservableCollection<ConsoleModel> ListConsole
        {
            get { return listConsole; }
            set
            {
                listConsole = value;
                OnPropertyChanged(nameof(ListConsole));
            }
        }
        //Armazena o consolo selecionado
        private ConsoleModel consoleSelectedItem;
        public ConsoleModel ConsoleSelectedItem
        {
            get { return consoleSelectedItem; }
            set
            {
                if (value == null)
                    return;
                MessageBoxSaveWarning();
                ConsoleBackup = new ConsoleModel();
                if (ConsoleSelectedItem != null)
                    ConsoleSelectedItem.PropertyChanged -= ConsoloSelectedItem_PropertyChanged;
                consoleSelectedItem = value;
                ConsoleSelectedItem.PropertyChanged += ConsoloSelectedItem_PropertyChanged;
                OnPropertyChanged(nameof(ConsoleSelectedItem));
                //LER_DADOS_CONSOLO();
                Console_Options(0);
                LoadDWG(ConsoleSelectedItem.Nomcons);
                ChangedTag = false;
            }
        }
        //Lista com as bitolas que aparecem nos comboboxes
        private List<double> reinforcingbar;
        public List<double> ReinforcingBar
        {
            get { return reinforcingbar; }
            set { reinforcingbar = value; OnPropertyChanged(nameof(ReinforcingBar)); }
        }

        private string unidBitolaTirante;
        public string UnidBitolaTirante
        {
            get { return unidBitolaTirante; }
            set { unidBitolaTirante = value; OnPropertyChanged(nameof(UnidBitolaTirante)); }
        }

        private string unidBitolaCostura;
        public string UnidBitolaCostura
        {
            get { return unidBitolaCostura; }
            set { unidBitolaCostura = value; OnPropertyChanged(nameof(UnidBitolaCostura)); }
        }

        private string unidBitolaTransversal;
        public string UnidBitolaTransversal
        {
            get { return unidBitolaTransversal; }
            set { unidBitolaTransversal = value; OnPropertyChanged(nameof(UnidBitolaTransversal)); }
        }

        private string unidBitolaConstrutiva;
        public string UnidBitolaConstrutiva
        {
            get { return unidBitolaConstrutiva; }
            set { unidBitolaConstrutiva = value; OnPropertyChanged(nameof(UnidBitolaConstrutiva)); }
        }

        #endregion

        #region Prop
        //Variável para a janela de desenhos
        public DrawingComponent _DrawingComponent { get; set; }
        public StringBuilder BuildingName { get; set; }
        public StringBuilder BuildingPath { get; set; }
        public ConsoleModel ConsoleBackup { get; set; }
        public bool ChangedTag { get; set; }
        public Thread thread { get; set; }
        #endregion

        //Construtor
        public RINTCONS_VM(DrawingComponent drawingComponent)
        {
            ChangedTag = false;
            _DrawingComponent = drawingComponent;
            BuildingName = new StringBuilder(260);
            BuildingPath = new StringBuilder(260);
            ReinforcingBar = new List<double>();
            INITIAL_LOAD();
            CONSOLOS_LER();
            LoadReinforcingBarOptions();
            CarregarSimboloUnidades();
        }

        #region Methods
        //Mensagem que será mostrada quando houver alteração nos dados do consolo e o 
        //usuário tenta fechar a janela ou trocar de consolo sem salvar as alterações
        public void MessageBoxSaveWarning()
        {
            if (ChangedTag == true)
            {
                MessageBoxResult result = MessageBox.Show("Deseja salvar as alterações realizadas neste consolo?", "TQS", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Console_Options(2);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
        }

        //Carrega os consolos disponíveis, com seus respectivos nomes, larguras de apoio
        //e quantidade de arquivos com o mesmo nome (IDuplo)
        public void CONSOLOS_LER()
        {
            int ISTAT = 0;
            int ISTCONS = 0;
            StringBuilder NOMCONS = new StringBuilder(256);
            int IDUPLO = 0;
            double ALARGAPO = 0;
            //Lendo arquivo CONSOLOS.DAT no disco
            RINTCONS.LERDISCO(ref ISTAT);
            //Lendo dados dos consolos
            RINTCONS.PREPLER();
            //Instanciar memória para os dados dos consolos
            ListConsole = new ObservableCollection<ConsoleModel>();
            while (ISTCONS == 0)
            {
                RINTCONS.LERPROX(NOMCONS, 0, ref IDUPLO, ref ALARGAPO, ref ISTCONS);
                if (ISTCONS != 0) break;
                ConsoleModel cons = new ConsoleModel();
                cons.Nomcons = NOMCONS.ToString();
                cons.Alargapo = ALARGAPO;
                cons.IDuplo = IDUPLO + 1;
                ListConsole.Add(cons);
            }
        }

        public void INITIAL_LOAD()
        {
            StringBuilder ACESSOLSTRING = new StringBuilder(256);
            int IPID = 0;
            ACESSOL_METHODS.ACL_INIACESSO();
            ACESSOL_METHODS.ACLGETPID(ref IPID);
            if (IPID != 0)
                RINTCONS.DEFPID(ref IPID);
            READ_BUILDING();
        }

        public void READ_BUILDING()
        {
            int ISTEDI = 0;
            int IPIS = 0;
            double COTI = 0;
            int IATI = 0;
            int IATF = 0;
            int ISTAT = 0;
            int IGNORE = 0;
            int NPRJ = 0;
            ACESSOL_METHODS.ACLIST(ref ISTEDI);
            ACESSOL_METHODS.ACLINICX(ref ISTAT);
            if (ISTAT != 0)
                return;
            ACESSOL_METHODS.ACLDED(BuildingName, IGNORE, ref NPRJ, ref IPIS, ref COTI, ref IATI, ref IATF);
            SelectedBuildingName = BuildingName.ToString();
            ACESSOL_METHODS.ACLDIRPRECONS(BuildingPath, IGNORE);
            if (ISTEDI == 0)
                ACESSOL_METHODS.ACLFIM();
        }

        protected void ConsoloSelectedItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Verifica se houve alteração no consolo antes de executar a tarefa assíncrona no escopo
            if (ConsoleSelectedItem != null && ConsoleBackup != null && ConsoleSelectedItem.Equals(ConsoleBackup) == false && ConsoleSelectedItem.Nomcons == ConsoleBackup.Nomcons)
            {
                //Método assíncrono para gravar um DWG com as edições do usuário e mostrar na tela
                //Sem que trave a aplicação
                _ = MakePreview();
            }
        }

        public async Task MakePreview()
        {
            //Gerar preview da alteração do usuário
            await Task.Run(() => Console_Options(1));
            //Carregar DWG alterado pelo usuário
            LoadDWG("PREVIEW");
            ChangedTag = true;
        }

        //Carrega o desenho DWG do consolo selecionado
        public void LoadDWG(string nomcons)
        {
            string file = @"";
            //Diretório obtido por ACESSOL_METHODS.ACLDIRPRECONS
            file = Path.Combine(file, BuildingPath.ToString());
            //Arquivo DWG selecionado na aplicação
            string complement = nomcons + "_" + ConsoleSelectedItem.IDuplo.ToString("00") + ".DWG";
            //Montagem do caminho completo para abrir o arquivo DWG
            file = Path.Combine(file, complement);
            _DrawingComponent.Open(file);
            _DrawingComponent.ZoomExtents();
        }

        public void Console_Options(byte opcao)
        {
            //Variáveis para RINTCONS_PEGRESULT
            int ITIPOCONS = 0; int ITIPOCALC = 0; int ISTCONS = 0; double ISTAT = 0;
            double ASTIR = 0; double ASMINTIR = 0; int NRAMOSTIR = 0; double BITOLATIR = 0;
            double ASCOST = 0; int NBITCOST = 0; double BITOLCOST = 0; double ESPCOST = 0;
            int NBITTRANSV = 0; int NRAMOSTRANS = 0; double BITOLTRANS = 0; double ASTRANSV = 0;
            double ESPTRANSV = 0; double TALWU = 0; double TALWD = 0;
            StringBuilder MSGERRO = new StringBuilder(256);

            //Variáveis para RINTCONS_PEGCRIT
            double FCK = 0; double COBR = 0; double GAMAC = 0; double GAMAS = 0; double GAMAF = 0;
            double GAMANCONSOLO = 0; int INORMADETLHA = 0; double FHORIMINCONS = 0; int ICONCRCONSOL = 0;
            double TXMECMINCONS = 0; int ITPANCORCONS = 0; int IFORCONSMCUR = 0; double BITMNTIRCONS = 0; double BITMNCOSCONS = 0;
            double ESPMNCOSCONS = 0; double BITMNTRACONS = 0; double BITMXTRACONS = 0; double BITCONSTCONS = 0; double CMNDBRTRCONS = 0;
            double ESCCONLONG = 0; double ESCCONTRAN = 0;

            //Variável para RINTCONS_NOMEPADRAODWG
            StringBuilder NOMEDWG = new StringBuilder(260);


            //Opção:
            //0 - Ler
            //1 - Atualizar Desenho
            //2 - Gravar no disco

            if (opcao == 0)
            {
                Ler(ConsoleSelectedItem);
                Populate_controls();
            }
            else if (opcao == 1)
            {
                Atualizar();
            }
            else
            {
                Gravar();
            }

            void Populate_controls()
            {
                ConsoleSelectedItem.Nramostir = NRAMOSTIR;
                ConsoleSelectedItem.Bitolatir = BITOLATIR;
                ConsoleSelectedItem.Nbitcost = NBITCOST;
                ConsoleSelectedItem.Bitolcost = BITOLCOST;
                ConsoleSelectedItem.Nbittransv = NBITTRANSV;
                ConsoleSelectedItem.Nramostrans = NRAMOSTRANS;
                ConsoleSelectedItem.Bitoltrans = BITOLTRANS;
                ConsoleSelectedItem.Bitconstcons = BITCONSTCONS; //Verificar se é esta variável mesmo.
                //Cópia independente do consolo selecionado
                ConsoleBackup = ConsoleSelectedItem.DeepCopy();
            }

            //Opcao 0
            void Ler(ConsoleModel consolo, string nomcons = "")
            {

                StringBuilder Nomcons = new StringBuilder(260);
                nomcons = nomcons == "" ? ConsoleSelectedItem.Nomcons : nomcons;
                Nomcons.Append(nomcons);
                int iduplo = consolo.IDuplo - 1;
                double alargapo = consolo.Alargapo;
                RINTCONS.LERCONSOLO(Nomcons, 0, ref iduplo, ref alargapo, ref ISTCONS);
                if (ISTCONS != 0)
                {
                    MessageBox.Show("Erro ao ler consolo" + consolo.Nomcons + consolo.IDuplo.ToString(), "TQS", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                RINTCONS.PEGRESULT(ref ITIPOCONS, ref ITIPOCALC, ref ASTIR, ref ASMINTIR, ref NRAMOSTIR, ref BITOLATIR, ref ASCOST,
                                   ref NBITCOST, ref BITOLCOST, ref ESPCOST, ref NBITTRANSV, ref NRAMOSTRANS, ref BITOLTRANS,
                                   ref ASTRANSV, ref ESPTRANSV, ref TALWU, ref TALWD, ref ISTAT, MSGERRO, 0);

                RINTCONS.PEGCRIT(ref FCK, ref COBR, ref GAMAC, ref GAMAS, ref GAMAF, ref GAMANCONSOLO, ref INORMADETLHA, ref FHORIMINCONS,
                     ref ICONCRCONSOL, ref TXMECMINCONS, ref ITPANCORCONS, ref IFORCONSMCUR, ref BITMNCOSCONS, ref BITMNCOSCONS,
                     ref ESPMNCOSCONS, ref BITMNTRACONS, ref BITMXTRACONS, ref BITCONSTCONS, ref CMNDBRTRCONS, ref ESCCONLONG, ref ESCCONTRAN);
            }

            //Opcao 1
            void Atualizar()
            {
                StringBuilder Nomcons = new StringBuilder(260);
                Nomcons.Append(ConsoleSelectedItem.Nomcons);
                int iduplo = ConsoleSelectedItem.IDuplo - 1;
                double alargapo = ConsoleSelectedItem.Alargapo;

                RINTCONS.LERCONSOLO(Nomcons, 0, ref iduplo, ref alargapo, ref ISTCONS);
                if (ISTCONS != 0)
                {
                    MessageBox.Show("Erro ao ler consolo" + ConsoleSelectedItem.Nomcons + ConsoleSelectedItem.IDuplo.ToString(), "TQS", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                RINTCONS.PEGRESULT(ref ITIPOCONS, ref ITIPOCALC, ref ASTIR, ref ASMINTIR, ref NRAMOSTIR, ref BITOLATIR, ref ASCOST,
                                   ref NBITCOST, ref BITOLCOST, ref ESPCOST, ref NBITTRANSV, ref NRAMOSTRANS, ref BITOLTRANS,
                                   ref ASTRANSV, ref ESPTRANSV, ref TALWU, ref TALWD, ref ISTAT, MSGERRO, 0);

                RINTCONS.PEGCRIT(ref FCK, ref COBR, ref GAMAC, ref GAMAS, ref GAMAF, ref GAMANCONSOLO, ref INORMADETLHA, ref FHORIMINCONS,
                     ref ICONCRCONSOL, ref TXMECMINCONS, ref ITPANCORCONS, ref IFORCONSMCUR, ref BITMNCOSCONS, ref BITMNCOSCONS,
                     ref ESPMNCOSCONS, ref BITMNTRACONS, ref BITMXTRACONS, ref BITCONSTCONS, ref CMNDBRTRCONS, ref ESCCONLONG, ref ESCCONTRAN);

                //IMPLEMENTAR AQUI SALVAR E ATUALIZAR O DESENHO

                NRAMOSTIR = ConsoleSelectedItem.Nramostir;
                BITOLATIR = ConsoleSelectedItem.Bitolatir;
                NBITCOST = ConsoleSelectedItem.Nbitcost;
                BITOLCOST = ConsoleSelectedItem.Bitolcost;
                NBITTRANSV = ConsoleSelectedItem.Nbittransv;
                NRAMOSTRANS = ConsoleSelectedItem.Nramostrans;
                BITOLTRANS = ConsoleSelectedItem.Bitoltrans;
                BITCONSTCONS = ConsoleSelectedItem.Bitconstcons;

                RINTCONS.DEFRESULT(ref NRAMOSTIR, ref BITOLATIR, ref NBITCOST, ref BITOLCOST, ref ESPCOST, ref NBITTRANSV, ref NRAMOSTRANS,
                                    ref BITOLTRANS, ref ASTRANSV, ref ESPTRANSV);

                RINTCONS.DEFCRIT(ref FCK, ref COBR, ref GAMAC, ref GAMAS, ref GAMAF, ref GAMANCONSOLO, ref INORMADETLHA, ref FHORIMINCONS,
                                 ref ICONCRCONSOL, ref TXMECMINCONS, ref ITPANCORCONS, ref IFORCONSMCUR, ref BITMNTIRCONS, ref BITMNCOSCONS, ref ESPMNCOSCONS,
                                 ref BITMNTRACONS, ref BITMXTRACONS, ref BITCONSTCONS, ref CMNDBRTRCONS, ref ESCCONLONG, ref ESCCONTRAN);

                NOMEDWG.Append("PREVIEW_" + (iduplo + 1).ToString("00"));

                RINTCONS.GERARDWG(NOMEDWG, 0, ref ISTCONS);
                INITIAL_LOAD();
                CONSOLOS_LER();
                Ler(ConsoleBackup);
            }

            //Opcao 2
            void Gravar()
            {
                Ler(ConsoleSelectedItem);
                StringBuilder Nomcons = new StringBuilder(260);
                Nomcons.Append(ConsoleSelectedItem.Nomcons);
                int iduplo = ConsoleSelectedItem.IDuplo - 1;
                double alargapo = ConsoleSelectedItem.Alargapo;

                RINTCONS.LERCONSOLO(Nomcons, 0, ref iduplo, ref alargapo, ref ISTCONS);
                if (ISTCONS != 0)
                {
                    MessageBox.Show("Erro ao ler consolo" + ConsoleSelectedItem.Nomcons + ConsoleSelectedItem.IDuplo.ToString(), "TQS", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                RINTCONS.PEGRESULT(ref ITIPOCONS, ref ITIPOCALC, ref ASTIR, ref ASMINTIR, ref NRAMOSTIR, ref BITOLATIR, ref ASCOST,
                                   ref NBITCOST, ref BITOLCOST, ref ESPCOST, ref NBITTRANSV, ref NRAMOSTRANS, ref BITOLTRANS,
                                   ref ASTRANSV, ref ESPTRANSV, ref TALWU, ref TALWD, ref ISTAT, MSGERRO, 0);

                RINTCONS.PEGCRIT(ref FCK, ref COBR, ref GAMAC, ref GAMAS, ref GAMAF, ref GAMANCONSOLO, ref INORMADETLHA, ref FHORIMINCONS,
                     ref ICONCRCONSOL, ref TXMECMINCONS, ref ITPANCORCONS, ref IFORCONSMCUR, ref BITMNCOSCONS, ref BITMNCOSCONS,
                     ref ESPMNCOSCONS, ref BITMNTRACONS, ref BITMXTRACONS, ref BITCONSTCONS, ref CMNDBRTRCONS, ref ESCCONLONG, ref ESCCONTRAN);

                //IMPLEMENTAR AQUI SALVAR E ATUALIZAR O DESENHO

                NRAMOSTIR = ConsoleSelectedItem.Nramostir;
                BITOLATIR = ConsoleSelectedItem.Bitolatir;
                NBITCOST = ConsoleSelectedItem.Nbitcost;
                BITOLCOST = ConsoleSelectedItem.Bitolcost;
                NBITTRANSV = ConsoleSelectedItem.Nbittransv;
                NRAMOSTRANS = ConsoleSelectedItem.Nramostrans;
                BITOLTRANS = ConsoleSelectedItem.Bitoltrans;
                BITCONSTCONS = ConsoleSelectedItem.Bitconstcons;

                RINTCONS.DEFRESULT(ref NRAMOSTIR, ref BITOLATIR, ref NBITCOST, ref BITOLCOST, ref ESPCOST, ref NBITTRANSV, ref NRAMOSTRANS,
                                    ref BITOLTRANS, ref ASTRANSV, ref ESPTRANSV);

                RINTCONS.DEFCRIT(ref FCK, ref COBR, ref GAMAC, ref GAMAS, ref GAMAF, ref GAMANCONSOLO, ref INORMADETLHA, ref FHORIMINCONS,
                                 ref ICONCRCONSOL, ref TXMECMINCONS, ref ITPANCORCONS, ref IFORCONSMCUR, ref BITMNTIRCONS, ref BITMNCOSCONS, ref ESPMNCOSCONS,
                                 ref BITMNTRACONS, ref BITMXTRACONS, ref BITCONSTCONS, ref CMNDBRTRCONS, ref ESCCONLONG, ref ESCCONTRAN);

                NOMEDWG = new StringBuilder(260);
                RINTCONS.NOMEPADRAODWG(NOMEDWG, 0);
                RINTCONS.GERARDWG(NOMEDWG, 0, ref ISTCONS);
                RINTCONS.GRVDISCO(ref ISTCONS);
            }
        }

        protected bool VerificarAlteracao()
        {
            return ConsoleSelectedItem.Equals(ConsoleBackup);
        }

        //Inicializa as bitolas que serão mostradas nas comboboxes
        protected void LoadReinforcingBarOptions()
        {
            ReinforcingBar.Add(4.2);
            ReinforcingBar.Add(5.0);
            ReinforcingBar.Add(6.3);
            ReinforcingBar.Add(8.0);
            ReinforcingBar.Add(10.0);
            ReinforcingBar.Add(12.5);
            ReinforcingBar.Add(16.0);
            ReinforcingBar.Add(20.0);
            ReinforcingBar.Add(25.0);
        }

        //Inicializa o símbolo de unidades das bitolas
        protected void CarregarSimboloUnidades()
        {
            unidBitolaTirante = "mm";
            unidBitolaCostura = "mm";
            unidBitolaTransversal = "mm";
            unidBitolaConstrutiva = "mm";
        }
        #endregion


        //NUNID.DLL
        //ACESSOL.DLL
        // SharedSizeGroup

    }
}
