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
    /*
     *  Translator is responsible for translate image into text
     **/
    public class Translator
    {
        public string readInput;
        public string readOutput;
        public string tesseract;
        public string imageName;
        public string logFile;
        public Translator(string imageName)
        {
            //read loggin information and paths out of the config file
            this.readInput = ConfigurationSettings.AppSettings["input"];
            this.readOutput = ConfigurationSettings.AppSettings["output"];
            this.tesseract = ConfigurationSettings.AppSettings["tesseract"];
            this.logFile = ConfigurationSettings.AppSettings["logfile"];
            this.imageName = imageName;
        }


        public string transLate()
        {
            //Define parameters for executing tesseract
            string outputName = Path.GetFileNameWithoutExtension(imageName);// +".txt";

            string pathToImage = readInput + "\\" + imageName;
            string pathToOutput = readOutput + "\\" + outputName;

            /*Supported languages: ara (Arabic), aze (Azerbauijani), bul (Bulgarian), cat (Catalan), ces (Czech), 
             * chi_sim (Simplified Chinese), chi_tra (Traditional Chinese), chr (Cherokee), 
             * dan (Danish), dan-frak (Danish (Fraktur)), deu (German), ell (Greek), eng (English), 
             * enm (Old English), epo (Esperanto), est (Estonian), fin (Finnish), fra (French), 
             * frm (Old French), glg (Galician), heb (Hebrew), hin (Hindi), hrv (Croation), 
             * hun (Hungarian), ind (Indonesian), ita (Italian), jpn (Japanese), kor (Korean), 
             * lav (Latvian), lit (Lithuanian), nld (Dutch), nor (Norwegian), pol (Polish), 
             * por (Portuguese), ron (Romanian), rus (Russian), slk (Slovakian), slv (Slovenian), 
             * sqi (Albanian), spa (Spanish), srp (Serbian), swe (Swedish), tam (Tamil), 
             * tel (Telugu), tgl (Tagalog), tha (Thai), tur (Turkish), ukr (Ukrainian), vie (Vietnamese)*/

            //define the language
            string language = "eng";
            
            //pull the command together
            //string command = tesseract+" " + pathToImage + " " + pathToOutput + " -l " + language;
            
            
            //create a new process
            Process pProcess = new Process();

            try
            {
                //add filename, parameter and additional information the the Start info of the process
                pProcess.StartInfo.FileName = tesseract;
                pProcess.StartInfo.Arguments = pathToImage + " " + pathToOutput + " -l " + language;
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.CreateNoWindow = true;
                DateTime startTime = DateTime.Now;
                pProcess.Start();                
                pProcess.WaitForExit();
                DateTime endTime = DateTime.Now;
                TimeSpan duration = endTime.Subtract(startTime);
                pProcess.Close();
                //write path, start time, end time and duration into the logfile
                File.Delete(pathToImage);
                using (StreamWriter writer = File.AppendText(logFile))
                {
                    writer.WriteLine("{0}|{1}|{2}|{3}", pathToImage, startTime.ToString(), endTime.ToString(), duration.Duration());

                }
                
            }
            catch (Exception ex)
            {
                //in case of error, write error message into the logfile
                using (StreamWriter writer = File.AppendText(logFile))
                {
                    writer.WriteLine(ex.Message);

                }
  
            }
            finally
            {
                //close the process
                pProcess.Close();
            }

            //return the path to the output file
            return pathToOutput;

        }
    }
}
