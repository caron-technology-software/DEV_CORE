#undef VERBOSE 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

using ProRob;

namespace Machine
{
    public class UWF
    {
        public static bool IsInstalled()
        {
            PowerShell ps = PowerShell.Create();

            ps.AddCommand(@"c:\windows\sysnative\uwfmgr.exe");

            try
            {
                var results = ps.Invoke();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsEnabled()
        {
            //-----------------------------------------------------------------------------------------
            //https://stackoverflow.com/questions/43134026/how-to-know-if-uwf-is-enabled-or-disabled
            //-----------------------------------------------------------------------------------------

            try
            {
                //root\standardcimv2\embedded
                ManagementScope scope = new ManagementScope(@"root\standardcimv2\embedded");

                using (ManagementClass mc = new ManagementClass(scope.Path.Path, "UWF_Filter", null))
                {
                    ManagementObjectCollection moc = mc.GetInstances();

                    foreach (ManagementObject mo in moc)
                    {
                        // Please make use of NextEnabled property in case you have enabled the UWF_Filter in the current session. 
                        //The value in CurrentEnabled is populated only when the UWF_Filter was enabled on system boot.
                        return (bool)mo.GetPropertyValue("CurrentEnabled");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return false;
        }

        public static void Enable()
        {
            try
            {
                (string[] output, _) = ProcessHelper.ExecuteShellCommand(@"c:\windows\sysnative\uwfmgr.exe filter enable");

#if VERBOSE
                foreach (var o in output)
                {
                    Console.WriteLine(o);
                }
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine($"[UWF EXCEPTION]\n\tMessage:{e.Message}\n\tSource:{e.Source}");
            }
        }

        public static void Disable()
        {
            try
            {
                (string[] output, _) = ProcessHelper.ExecuteShellCommand(@"c:\windows\sysnative\uwfmgr.exe filter disable");
#if VERBOSE
                foreach (var o in output)
                {
                    Console.WriteLine(o);
                }
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine($"[UWF EXCEPTION]\n\tMessage:{e.Message}\n\tSource:{e.Source}");
            }
        }
    }
}