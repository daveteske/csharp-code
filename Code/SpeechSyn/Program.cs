using System;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using NAudio.Wave;
using NAudio.Lame;
using System.IO;

namespace SampleSynthesis
{
    class Program
    {
        static void Main(string[] args)
        {

            // Initialize a new instance of the SpeechSynthesizer.  
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {

                  // This example demonstrates a basic use of Speech Synthesizer

                string stringToRead = "<speak version=\"1.0\"";
                    stringToRead += " xmlns=\"http://www.w3.org/2001/10/synthesis\"";
                    stringToRead += " xml:lang=\"en-US\">";
                //stringToRead += " This example demonstrates a basic use of Speech Synthesizer ";
                //stringToRead += " <break time=\"3s\"/>";
                //stringToRead += "  A <emphasis level=\"strong\"> second paragraph </emphasis> that is nearly as good as the first. ";
                stringToRead += " <p> This <emphasis level=\"strong\"> example </emphasis> demonstrates a basic use of Speech Synthesizer </p>";
                    stringToRead += " <p> A second paragraph that is nearly as good as the first. </p>";
                    //stringToRead += "<say-as type=\"date:mdy\"> 1/29/2009 </say-as>";
                    stringToRead += "</speak>";

                synth.Volume = 100;
                synth.Rate = 0;

                //var ms = new MemoryStream();
                //synth.SetOutputToWaveStream(ms);

                var abc = synth.GetInstalledVoices();
                foreach (var item in abc)
                {
                    Console.WriteLine(item.VoiceInfo.Name);
                }
                // synth.SelectVoice("Microsoft Zira Desktop");

                // Configure the audio output.   
                synth.SetOutputToDefaultAudioDevice();

                // Speak a string.  
                synth.SpeakSsml(stringToRead);

                //ConvertToMp3(ref ms, "myfile.mp3");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void ConvertToMp3(ref MemoryStream ms, string filename)
        {
            ms.Seek(0, SeekOrigin.Begin);

            using (var retMs = new MemoryStream())
            {
                using (var rdr = new WaveFileReader(ms))
                {
                    using (var wtr = new LameMP3FileWriter(filename, rdr.WaveFormat, LAMEPreset.VBR_50))
                    {
                        rdr.CopyTo(wtr);
                    }
                }
            }
        }
    }
}