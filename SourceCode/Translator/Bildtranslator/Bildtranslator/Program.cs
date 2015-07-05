using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace Bildtranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            string readInput = ConfigurationManager.AppSettings["input"];
            string readOuput = ConfigurationManager.AppSettings["output"];


            string imageName = "IMG_20140723_113303.jpg";
            string outputName = "IMG_20140723_113303.txt";

            string pathToImage = readInput+"\\"+imageName;
            string pathToOutput = readOuput+"\\"+outputName;
            
            /*ara (Arabic), aze (Azerbauijani), bul (Bulgarian), cat (Catalan), ces (Czech), 
             * chi_sim (Simplified Chinese), chi_tra (Traditional Chinese), chr (Cherokee), 
             * dan (Danish), dan-frak (Danish (Fraktur)), deu (German), ell (Greek), eng (English), 
             * enm (Old English), epo (Esperanto), est (Estonian), fin (Finnish), fra (French), 
             * frm (Old French), glg (Galician), heb (Hebrew), hin (Hindi), hrv (Croation), 
             * hun (Hungarian), ind (Indonesian), ita (Italian), jpn (Japanese), kor (Korean), 
             * lav (Latvian), lit (Lithuanian), nld (Dutch), nor (Norwegian), pol (Polish), 
             * por (Portuguese), ron (Romanian), rus (Russian), slk (Slovakian), slv (Slovenian), 
             * sqi (Albanian), spa (Spanish), srp (Serbian), swe (Swedish), tam (Tamil), 
             * tel (Telugu), tgl (Tagalog), tha (Thai), tur (Turkish), ukr (Ukrainian), vie (Vietnamese)*/

            string language = "eng";
            string tesseract = ConfigurationManager.AppSettings["tesseract"];
            //string tesseract = @"C:\Users\Roger\Documents\GitHub\Bildanalyse\SourceCode\Translator\Bildtranslator\Bildtranslator\Tesseract-OCR\tesseract.exe";
            string command =  tesseract+" " + pathToImage + " " + pathToOutput + " -l " + language;
            Console.WriteLine(command);
            //System.Diagnostics.Process.Start(command);

            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process Process;

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + command);

            ProcessInfo.CreateNoWindow = false;
            ProcessInfo.UseShellExecute = false;

            Process = Process.Start(ProcessInfo);
            Process.WaitForExit();

            ExitCode = Process.ExitCode;
            Process.Close();

            //MessageBox.Show("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");

        }
    }
}
