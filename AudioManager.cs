using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.Compression;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecorder
{
    public class AudioManager
    {
        public string SaveFilePath { get; set; }
        public string MyNewAudioFileName { get; set; }
        private WaveInEvent waveSource = null;

        private WaveFileWriter waveFile = null;

        public void InitRecorder()
        {
            SaveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\MyRecordedWaves\";
            if (!Directory.Exists(SaveFilePath))
                Directory.CreateDirectory(SaveFilePath);
        }

        public void OpenRecordedAudioFolder() => Process.Start("explorer.exe", SaveFilePath);

        public void OpenRecordedAudioFile()
        {
            if (Directory.Exists(SaveFilePath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = MyNewAudioFileName,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
        }

        public void StartRecording()
        {
            MyNewAudioFileName = SaveFilePath + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + ".wav";
            waveSource = new WaveInEvent();
            waveSource.WaveFormat = new WaveFormat(44100, 1);
            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveFile = new WaveFileWriter(MyNewAudioFileName, waveSource.WaveFormat);
            waveSource.StartRecording();
        }

        private void waveSource_RecordingStopped(object? sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }

        private void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            //WaveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);

            //2

            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        public void StopRecording()
        {
            if (waveSource != null)
            {
                waveSource.StopRecording();
            }
        }
    }
}