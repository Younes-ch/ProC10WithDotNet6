Imports CarLibrary

Module Program
    Sub Main(args As String())
        Console.WriteLine("***** VB CarLibrary Client App *****")
        Dim myMiniVan As New MiniVan()
        myMiniVan.TurboBoost()

        Dim mySportsCar As New SportsCar()
        mySportsCar.TurboBoost()
        Console.ReadLine()

        Dim dreamCar = New PerformanceCar()
        dreamCar.PetName = "Hank"
        dreamCar.TurboBoost()

        Console.ReadLine()
    End Sub
End Module
