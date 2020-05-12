using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TQS.ViewModel;

namespace TQS.Model
{
    public class ConsoleModel : VMObject
    {
        private string nomcons;
        public string Nomcons
        {
            get { return nomcons; }
            set { nomcons = value; OnPropertyChanged(nameof(Nomcons)); }
        }


        private double alargapo;
        public double Alargapo
        {
            get { return alargapo; }
            set { alargapo = value; OnPropertyChanged(nameof(Alargapo)); }
        }

        private int iduplo;
        public int IDuplo
        {
            get { return iduplo; }
            set { iduplo = value; OnPropertyChanged(nameof(IDuplo)); }
        }

        private double bitolcost;
        public double Bitolcost
        {
            get { return bitolcost; }
            set { bitolcost = value; OnPropertyChanged(nameof(Bitolcost)); }
        }

        private double bitolatir;
        public double Bitolatir
        {
            get { return bitolatir; }
            set { bitolatir = value; OnPropertyChanged(nameof(Bitolatir)); }
        }

        private double bitoltrans;
        public double Bitoltrans
        {
            get { return bitoltrans; }
            set { bitoltrans = value; OnPropertyChanged(nameof(Bitoltrans)); }
        }

        private int nbitcost;
        public int Nbitcost
        {
            get { return nbitcost; }
            set { nbitcost = value; OnPropertyChanged(nameof(Nbitcost)); }
        }

        private int nbittransv;
        public int Nbittransv
        {
            get { return nbittransv; }
            set { nbittransv = value; OnPropertyChanged(nameof(Nbittransv)); }
        }

        private int nramostir;
        public int Nramostir
        {
            get { return nramostir; }
            set { nramostir = value; OnPropertyChanged(nameof(Nramostir)); }
        }

        private int nramostrans;
        public int Nramostrans
        {
            get { return nramostrans; }
            set { nramostrans = value; OnPropertyChanged(nameof(Nramostrans)); }
        }

        private double bitconstcons;
        public double Bitconstcons
        {
            get { return bitconstcons; }
            set { bitconstcons = value; OnPropertyChanged(nameof(Bitconstcons)); }
        }

        private bool alterado;
        public bool Alterado
        {
            get { return alterado; }
            set { alterado = value; OnPropertyChanged(nameof(Alterado));}
        }


        //Atribui a uma variável uma cópia do CONSOLO_TAG em outro endereço de memória
        //Ao invés de um ponteiro para o mesmo endereço de memória
        public ConsoleModel DeepCopy()
        {
            ConsoleModel clone = (ConsoleModel)this.MemberwiseClone();
            clone.Nomcons = String.Copy(Nomcons);
            clone.Alargapo = Alargapo;
            clone.IDuplo = IDuplo;
            clone.Bitolcost = Bitolcost;
            clone.Bitolatir = Bitolatir;
            clone.Bitoltrans = Bitoltrans;
            clone.Nbitcost = Nbitcost;
            clone.Nbittransv = Nbittransv;
            clone.Nramostir = Nramostir;
            clone.Nramostrans = Nramostrans;
            clone.Bitconstcons = Bitconstcons;
            return clone;
        }

        //Compara dois objetos do mesmo tipo
        public override bool Equals (object o)
        {
            if (o is ConsoleModel)
            {
                ConsoleModel obj = (ConsoleModel)o;
                if (this.Nramostir == obj.Nramostir &&
                    this.Bitolatir == obj.Bitolatir &&
                    this.Nbitcost == obj.Nbitcost &&
                    this.Bitolcost == obj.Bitolcost &&
                    this.Nbittransv == obj.Nbittransv &&
                    this.Bitoltrans == obj.Bitoltrans &&
                    this.bitconstcons == obj.Bitconstcons)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class CRIT_TAG
    {
        public double Fck { get; set; }
        public double Cobr { get; set; }
        public double Gamac { get; set; }
        public double Gamaf { get; set; }
        public double GamaNConsolo { get; set; }
        public long INormadetlha { get; set; }
        public double FHorimincons { get; set; }
        public long IConcrConsol { get; set; }
        public double TxmecminCons { get; set; }
        public long ItpancorCons { get; set; }
        public long IForconsmcur { get; set; }
        public double Bitmntircons { get; set; }
        public double Bitmncoscons { get; set; }
        public double Espmncoscons { get; set; }
        public double Bitmntracons { get; set; }
        public double Bitmxtracons { get; set; }
        public double Bitconstcons { get; set; }
        public double CmndbrtrCons { get; set; }
        public double Escconlong { get; set; }
        public double Esccontran { get; set; }
    }

    public class RESULT_TAG
    {
        public long ITIPOCONS { get; set; }
        public long ITIPOCALC { get; set; }
        public double ASTIR { get; set; }
        public double ASMINTIR { get; set; }
        public long NRAMOSTIR { get; set; }
        public double BITOLATIR { get; set; }
        public double ASCOST { get; set; }
        public long NBITCOST { get; set; }
        public double BITOLCOST { get; set; }
        public double ESPCOST { get; set; }
        public long NBITTRANSV { get; set; }
        public long NRAMOSTRANS { get; set; }
        public double BITOLTRANS { get; set; }
        public double ASTRANSV { get; set; }
        public double ESPTRANSV { get; set; }
        public double TALWU { get; set; }
        public double TALWD { get; set; }
        public double ISTAT { get; set; }
        public StringBuilder MSGERRO { get; set; }
        public int ARGFAN { get; set; }
    }

    public class EditTable
    {
        public string Categoria { get; set; }
        public string StrNumRamos { get; set; }
        public long LngNumRamos { get; set; }
        public string StrNumEstrCos { get; set; }
        public double DblBitolaCos { get; set; }
        public string StrNumEstrTrans { get; set; }
        public double DblBitolaTrans { get; set; }
        public int MyProperty { get; set; }
    }

}
