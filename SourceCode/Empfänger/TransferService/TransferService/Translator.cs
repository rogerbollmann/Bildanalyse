using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace TransferService
{
    class Translator
    {
        string readInput;
        string readOutput;
        string tesseract;
        string imageName;
        public Translator(string imageName)
        {
            this.readInput = ConfigurationSettings.AppSettings["input"];
            this.readOutput = ConfigurationSettings.AppSettings["output"];
            this.tesseract = ConfigurationSettings.AppSettings["tesseract"];
            //this.tesseract = @"C:\Program Files (x86)\Tesseract-OCR\tesseract.exe";
            //this.readInput = @"C:\Users\Roger\Pictures\Input";
            //this.readOutput = @"C:\Users\Roger\Documents\GitHub\Bildanalyse\SourceCode\Translator\Output";
            this.imageName = imageName;
        }


        public string transLate()
        {
            //string readInput = ConfigurationManager.AppSettings["input"];
            //string readOuput = ConfigurationManager.AppSettings["output"];


            //string imageName = "IMG_20140723_113303.jpg";
            string outputName = Path.GetFileNameWithoutExtension(imageName)+".txt";

            string pathToImage = readInput + "\\" + imageName;
            string pathToOutput = readOutput + "\\" + outputName;

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
            //string tesseract = ConfigurationManager.AppSettings["tesseract"];
            //string tesseract = @"C:\Users\Roger\Documents\GitHub\Bildanalyse\SourceCode\Translator\Bildtranslator\Bildtranslator\Tesseract-OCR\tesseract.exe";

            string command = tesseract+" " + pathToImage + " " + pathToOutput + " -l " + language;
            //Console.WriteLine(command);
            //System.Diagnostics.Process.Start(command);
            
            int ExitCode;
            
            Process pProcess = new Process();
            pProcess.StartInfo.FileName = tesseract;
            pProcess.StartInfo.Arguments = pathToImage + " " + pathToOutput + " -l " + language;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.StartInfo.RedirectStandardError = true;
            pProcess.StartInfo.RedirectStandardOutput = true;

            pProcess.Start();
            //pProcess.BeginErrorReadLine();
            string stdError = pProcess.StandardError.ReadToEnd();
            Console.WriteLine(stdError);
            ExitCode = pProcess.ExitCode;
            pProcess.WaitForExit();
            
            Console.WriteLine("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");
            pProcess.Close();

            File.Delete(pathToImage);
            return pathToOutput;
        }
    }
}
