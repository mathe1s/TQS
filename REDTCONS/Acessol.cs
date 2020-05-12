using System.Runtime.InteropServices;
using System.Text;

namespace TQS.AcessoN
{
    internal class ACESSOL
    {
        public const int MAXDIRED = 260;

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACL_INIACESSO@0")]
        public static extern void ACL_INIACESSO();

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACL_FIMACESSO@0")]
        public static extern void ACL_FIMACESSO();

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLIST@4")]
        public static extern void ACL_ACLIST(ref int ISTAT);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLPROGR@8")]
        public static extern void ACLPROGR(StringBuilder exec, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLSUPOR@8")]
        public static extern void ACLSUPOR(StringBuilder supo, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLPREFE@8")]
        public static extern void ACLPREFE(StringBuilder user, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLRAIZ@8")]
        public static extern void ACLRAIZ(StringBuilder raiz, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLINICX@4")]
        public static extern void ACLINICX(ref int ISTAT);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLDED@28")]
        public static extern void ACLDED(StringBuilder tite, int ARG_FAN, ref int nprj, ref int ipis, ref double coti, ref int iati, ref int iatf);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLSEED@8")]
        public static extern void ACLSEED(StringBuilder seed, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLNFC@4")]
        public static extern void ACLNFC(ref int nvfck);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLFCP@12")]
        public static extern void ACLFCP(ref int ind, ref double fck, ref int ipiso);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLEXECPROG@40")]
        public static extern void ACLEXECPROG(StringBuilder comando, int ARG_FAN1, StringBuilder fstdin, int ARG_FAN2, StringBuilder fstdout, int ARG_FAN3, StringBuilder dir, int ARG_FAN4, ref int iespera, ref int istat);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLCLAGRESS@16")]
        public static extern void ACLCLAGRESS(ref int iclagress, ref int iclimaseco, ref int icontrolqual, ref int iconcrprot);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLFCV@4")]
        public static extern void ACLFCV(ref double fckvig);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_PFRACHA@28")]
        public static extern void PFRACHA(ref int iaplic, StringBuilder arqori, int ARG_FAN1, StringBuilder arqmod, int ARG_FAN2, ref int imod, ref int imodr);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLDRP@8")]
        public static extern void ACLDRP(StringBuilder drpl, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLDRG@8")]
        public static extern void ACLDRG(StringBuilder drgr, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLDRU@8")]
        public static extern void ACLDRU(StringBuilder drfu, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLDRO@8")]
        public static extern void ACLDRO(StringBuilder drpo, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLDIRSISE@8")]
        public static extern void ACLDIRSISE(StringBuilder dirsise, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLDIRTEMP@8")]
        public static extern void ACLDIRTEMP(StringBuilder dirtmp, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLIEFEITOINCREM@4")]
        public static extern void ACLIEFEITOINCREM (ref int iefeitoincrem);
        
        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLIPAIS@4")]
        public static extern void ACLIPAIS(ref int IPAIS);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLACHARPAIS@20")]
        public static extern void ACLACHARPAIS(ref int IPAIS, StringBuilder nomepais, int ARG_FAN1, StringBuilder prefixo, int ARG_FAN2);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_acllernorma@8")]
        public static extern void acllernorma(StringBuilder arqnorma, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLIDENTNORMA@12")]
        public static extern void ACLIDENTNORMA(ref int inorma, StringBuilder strnorma, int ARG_FAN);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLLERESFORCARAC@4")]
        public static extern void ACLLERESFORCARAC(ref int icarac);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACLALV@4")]
        public static extern void aclalv(ref int icra);

        [DllImport(@"ACESSOL.DLL", EntryPoint = "_ACL_PREDAD_LERIPREMOLD@4")]
        public static extern void ACL_PREDAD_LERIPREMOLD(ref int ipremold);
    }
}
