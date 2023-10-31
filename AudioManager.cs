using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Diagnostics;
using System.IO;

namespace SimpleRecorder
{
    public class AudioManager : IDisposable
    {
        public string SaveFilePath { get; private set; }
        public string MyNewAudioFileName { get; private set; }
        private WaveInEvent waveSource = null;
        private WaveFileWriter waveFile = null;

        public void InitRecorder()
        {
            SaveFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MyRecordedWaves");
            Directory.CreateDirectory(SaveFilePath); // CreateDirectory is a no-op if directory exists
        }

        public void OpenRecordedAudioFolder() => Process.Start("explorer.exe", SaveFilePath);

        public void OpenRecordedAudioFile()=>
                Process.Start("explorer.exe", MyNewAudioFileName);
            
        

        public void StartRecording()
        {
            MyNewAudioFileName = Path.Combine(SaveFilePath, $"{DateTime.UtcNow:yyyyMMdd_HHmmss}.wav");
            waveSource = new WaveInEvent();
            waveSource.WaveFormat = new WaveFormat(44100, 1);
            waveSource.DataAvailable += waveSource_DataAvailable;
            waveSource.RecordingStopped += waveSource_RecordingStopped;

            waveFile = new WaveFileWriter(MyNewAudioFileName, waveSource.WaveFormat);
            waveSource.StartRecording();
        }

        private void waveSource_RecordingStopped(object sender, StoppedEventArgs e) => DisposeResources();

        private void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        public void StopRecording()
        {
            waveSource?.StopRecording();
        }

        public void Dispose()
        {
            DisposeResources();
        }

        private void DisposeResources()
        {
            waveSource?.Dispose();
            waveSource = null;
            waveFile?.Dispose();
            waveFile = null;
        }
    }
}