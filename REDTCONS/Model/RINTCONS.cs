using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TQS.Consolos
{

    // Subrotinas existentes em RINTCONS.DLL

    public class RINTCONS
    {
        // Subrotinas para leitura e gravação de dados de todos os consolos no disco

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_LERDISCO@4")]
        public static extern void LERDISCO(ref int ISTAT);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_GRVDISCO@4")]
        public static extern void GRVDISCO(ref int ISTAT);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_DEFPID@4")]
        public static extern void DEFPID(ref int ISTAT);

        // Subrotinas para acesso aos dados dos consolos lidos

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_PREPLER@0")]
        public static extern void PREPLER();

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_LERPROX@20")]
        public static extern void LERPROX(StringBuilder NOMCONS, int ARG_FAN, ref int IDUPLO, ref double ALARGAPO, ref int ISTAT);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_LERCONSOLO@20")]
        public static extern void LERCONSOLO(StringBuilder NOMCONS, int ARG_FAN, ref int IDUPLO, ref double ALARGAPO, ref int ISTAT);

        // Subrotinas de cálculo e geração de desenho (DWG)

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_DIMENSIONAR@4")]
        public static extern void DIMENSIONAR(ref int ISTAT);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_NOMEPADRAODWG@8")]
        public static extern void NOMEPADRAODWG(StringBuilder NOMARQ, int ARG_FAN);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_GERARDWG@12")]
        public static extern void GERARDWG(StringBuilder NOMARQ, int ARG_FAN, ref int ISTAT);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_GERARDWGMEM@16")]
        public static extern void GERARDWGMEM(int PEDM, StringBuilder NOMARQ, int ARG_FAN, ref int ISTAT);

        // Subrotinas para leitura/gravação de critérios (bitola de armadura construtiva)

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_PEGCRIT@84")]
        public static extern void PEGCRIT(ref double FCK, ref double COBR, ref double GAMAC, ref double GAMAS, ref double GAMAF, ref double GAMANCONSOLO, ref int INORMADETLHA, ref double FHORIMINCONS, ref int ICONCRCONSOL, ref double TXMECMINCONS, ref int ITPANCORCONS, ref int IFORCONSMCUR, ref double BITMNTIRCONS, ref double BITMNCOSCONS, ref double ESPMNCOSCONS, ref double BITMNTRACONS, ref double BITMXTRACONS, ref double BITCONSTCONS, ref double CMNDBRTRCONS, ref double ESCCONLONG, ref double ESCCONTRAN);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_DEFCRIT@84")]
        public static extern void DEFCRIT(ref double FCK, ref double COBR, ref double GAMAC, ref double GAMAS, ref double GAMAF, ref double GAMANCONSOLO, ref int INORMADETLHA, ref double FHORIMINCONS, ref int ICONCRCONSOL, ref double TXMECMINCONS, ref int ITPANCORCONS, ref int IFORCONSMCUR, ref double BITMNTIRCONS, ref double BITMNCOSCONS, ref double ESPMNCOSCONS, ref double BITMNTRACONS, ref double BITMXTRACONS, ref double BITCONSTCONS, ref double CMNDBRTRCONS, ref double ESCCONLONG, ref double ESCCONTRAN);

        // Subrotinas para leitura/alteração de geometria

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_PEGGEOM@52")]
        public static extern void PEGGEOM(StringBuilder LISTPIL, int ARGFAN, ref double ALARGAPO, ref int IDUPLO, ref int NUMELEM, ref double B, ref double ACNS, ref double A, ref double H1, ref double H2, ref double DFS, ref int IMORRE, ref int ITIRCOSACALT);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_DEFGEOM@44")]
        public static extern void DEFGEOM(ref double ALARGAPO, ref int IDUPLO, ref int NUMELEM, ref double B, ref double ACNS, ref double A, ref double H1, ref double H2, ref double DFS, ref int IMORRE, ref int ITIRCOSACALT);

        // Subrotinas para leitura/alteração de cargas

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_PEGCARGAS@32")]
        public static extern void PEGCARGAS(StringBuilder DESCRLOCAL, int ARGFAN1, StringBuilder DESCRCOMB, int ARGFAN2, ref double FVK, ref double FHK, ref double VCADIC, ref double FNADIC);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_DEFCARGAS@16")]
        public static extern void DEFCARGAS(ref double FVK, ref double FHK, ref double VCADIC, ref double FNADIC);

        // Subrotinas para leitura/alteração de armaduras

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_PEGRESULT@80")]
        public static extern void PEGRESULT(ref int ITIPOCONS, ref int ITIPOCALC, ref double ASTIR, ref double ASMINTIR, ref int NRAMOSTIR, ref double BITOLATIR, ref double ASCOST, ref int NBITCOST, ref double BITOLCOST, ref double ESPCOST, ref int NBITTRANSV, ref int NRAMOSTRANS, ref double BITOLTRANS, ref double ASTRANSV, ref double ESPTRANSV, ref double TALWU, ref double TALWD, ref double ISTAT, StringBuilder MSGERRO, int ARGFAN);

        [DllImport(@"RINTCONS.DLL", EntryPoint = "_RINTCONS_DEFRESULT@40")]
        public static extern void DEFRESULT(ref int NRAMOSTIR, ref double BITOLATIR, ref int NBITCOST, ref double BITOLCOST, ref double ESPCOST, ref int NBITTRANSV, ref int NRAMOSTRANS, ref double BITOLTRANS, ref double ASTRANSV, ref double ESPTRANSV);

    }

    public class ACESSOL_METHODS
    {
        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLIST@4")]
        public static extern void ACLIST(ref int ISTAT);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLINICX@4")]
        public static extern void ACLINICX(ref int ISTAT);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLDED@28")]
        public static extern void ACLDED(StringBuilder NOME, int IGNORE, ref int NPRJ, ref int IPIS, ref double COTI, ref int IATI, ref int IATF);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLDIRPRECONS@8")]
        public static extern void ACLDIRPRECONS(StringBuilder NOME, int IGNORE);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACL_INIACESSO@0")]
        public static extern void ACL_INIACESSO();

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLGETPID@4")]
        public static extern void ACLGETPID(ref int IPID);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLFIM@0")]
        public static extern void ACLFIM();
    }
}