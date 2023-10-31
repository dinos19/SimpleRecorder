using SimpleRecorder;

AudioManager audio = new AudioManager();
audio.InitRecorder();
MainAppLoop();
void MainAppLoop()
{
    while (true)
    {
        Console.WriteLine("Choose an action.\n1) Open recorded audios folder\n2) Record a new audio\n3) Play last recorded\n4) Exit");
        var option = Console.ReadLine();
        switch (option)
        {
            case "1":
                audio.OpenRecordedAudioFolder();
                break;

            case "2":
                audio.StartRecording();
                Console.WriteLine("Starting recording.. Press anything to stop it.");
                Console.ReadLine();
                audio.StopRecording();
                break;

            case "3":
                audio.OpenRecordedAudioFile();
                break;

            case "4":
                return;

            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
}