using SimpleRecorder;

AudioManager audio = new AudioManager();
audio.InitRecorder();
MainAppLoop();
void MainAppLoop()
{
    Console.WriteLine("Choose an action.\n1) Open recorded audios folder\n2) Record a new audio\n3) Exit");
    var startingOption = Console.ReadLine();
    switch (startingOption)
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
            return;
    };

    SecondaryAppLoop();
}

void SecondaryAppLoop()
{
    Console.WriteLine("Choose an action.\n1) Play recorded\n2) Start over\n3) Exit");

    var afterRecordOptions = Console.ReadLine();
    switch (afterRecordOptions)
    {
        case "1":
            audio.OpenRecordedAudioFile();
            break;

        case "3":
            return;
    };

    MainAppLoop();
}