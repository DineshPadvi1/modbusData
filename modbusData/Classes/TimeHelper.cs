using System;
using System.IO;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;
using System.Windows;
using Uniproject.Classes;

namespace Uniproject.Classes.Model
{
    // 22/05/2024 : BhaveshT - 
    public static class TimeHelper
    {
        public static DateTime GetNistTime()
        {
            try
            {
                var client = new TcpClient("time.nist.gov", 13);
                using (var streamReader = new StreamReader(client.GetStream()))
                {
                    var response = streamReader.ReadToEnd();
                    var utcDateTimeString = response.Substring(7, 17);

                    if (clsFunctions_comman.FileType.ToLower().Contains("appdb"))
                    {
                        return DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                    }
                    else 
                    {
                        return DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                    }

                }
            } 
            catch (Exception ex)
            {
                if (ex.Message.Contains("String was not recognized as a valid DateTime.") == true)
                {
                    clsFunctions_comman.ErrorLog("Exception at TimeHelper: GetNistTime() - " + ex.Message);
                    return Convert.ToDateTime("17/06/2050");
                }

                else if (ex.Message.Contains("startIndex cannot be larger than length of string.\r\nParameter name: startIndex") == true)
                {
                    clsFunctions_comman.ErrorLog("Exception at TimeHelper: GetNistTime() - " + ex.Message);
                    return Convert.ToDateTime("17/06/2050");
                }

                else
                {
                    clsFunctions_comman.ErrorLog("Exception at TimeHelper: GetNistTime() - " + ex.Message);
                    return Convert.ToDateTime("17/06/2000");
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------------------------------------


    }


}

