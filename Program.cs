// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();

if (resp == "1")
{
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = int.Parse(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    Console.WriteLine(dataDate);
    // random number generator
    Random rnd = new Random();
    // create file
    StreamWriter sw = new StreamWriter("data.txt");
    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}



else if (resp == "2")
{
    string textFile = "data.txt";

    //checking to see if the txt file has been created yet
    //if it has not its returns the string "data file has not been found"
    //I added this even though not in the requirements because it displays to the user
    //what is happening when there is no data to parse.
    if (File.Exists(textFile))
    {
        //grabbing the text from the data file
        //https://www.c-sharpcorner.com/UploadFile/mahesh/how-to-read-a-text-file-in-C-Sharp/#:~:text=The%20File%20class%20provides%20two,and%20then%20closes%20the%20file.
        string[] lines = File.ReadAllLines(textFile);

        foreach (string line in lines)
        {
            //https://learn.microsoft.com/en-us/dotnet/csharp/how-to/parse-strings-using-split
            //seperating the string from the commas
            //I used the same method to remove the pipe deliminer
            string[] comma = line.Split(',');

            //validator to confirm the week was parsed properly if not then display the writeline of invalid date
            DateTime weekStartDate;
            if (!DateTime.TryParse(comma[0], out weekStartDate))
            {
                Console.WriteLine("Invalid date format");
                return;
            }

            //changing the numbers removed from the pipes from strings into ints using the ConvertAll method
            //https://www.geeksforgeeks.org/c-sharp-converting-an-array-of-one-type-to-an-array-of-another-type/
            int[] hoursSlept = Array.ConvertAll(comma[1].Split('|'), int.Parse);

            Console.WriteLine($"Week of {weekStartDate:MMM}, {weekStartDate:dd}, {weekStartDate:yyyy}");
            Console.WriteLine(" Su Mo Tu We Th Fr Sa");
            Console.WriteLine(" -- -- -- -- -- -- --");

            //iterating through each element of the hours slept array
            //the two in the curly bracers within the write indicated 
            //the minimum width of characters to format the numbers
            for (int i = 0; i < hoursSlept.Length; i++)
            {
                Console.Write($"{hoursSlept[i],2} ");
            }
            Console.WriteLine();
        }
    }
    //if no data file then send this message
    //this is only really needed when the program is first ran since after the
    //first section is run once it created the data.txt file=
    else
    {
        Console.WriteLine("data file not found");
    }
}
