'Program name: Smart Home Electric Savings Window Application
'Purpose: This application uses a text file that contains the 12 months of electric bill 
'savings after the smart home devices were activated.
'By: Katelyn Cooper

Option Strict On


Public Class SmartHomeApplication
    Private _intSizeOfArray As Integer = 11
    Private _strSavings(_intSizeOfArray) As String
    Private _decBill(_intSizeOfArray) As Decimal

    Private Sub SmartHomeApplication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'The SmartHomeApplication load event reads the savings text file and fill the months in the ComboBox
        'Initializes an instance of teh StreamReader object
        Dim objReader As IO.StreamReader
        Dim strSavingsAmount As String
        Dim intCount As Integer = 0
        Dim intFill As Integer

        btnDisplay.Visible = False

        lblTotal.Text = ""
        lblAverage.Text = ""
        lblHighestMonth.Text = ""
        'The if statement verifies that the file exists
        If IO.File.Exists("C:\Users\katel\Documents\GUI\projects\Smart_Home\savings.txt") = True Then
            objReader = IO.File.OpenText("C:\Users\katel\Documents\GUI\projects\Smart_Home\savings.txt")
            'The file is read line by line unitl the file is completed
            Do While objReader.Peek <> -1
                _strSavings(intCount) = objReader.ReadLine()
                strSavingsAmount = objReader.ReadLine()
                _decBill(intCount) = Convert.ToDecimal(strSavingsAmount)
                intCount += 1
            Loop
            objReader.Close()
            'The ComboBox object is filled with the Bill years
            For intFill = 0 To (_strSavings.Length - 1)
                cboMonths.Items.Add(_strSavings(intFill))
            Next
        Else
            MsgBox("The file is not available. Restart the program when the file is available", , "Error")
            Close()
        End If
    End Sub

    Private Sub cboMonths_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMonths.SelectedIndexChanged
        'the cboMonths selectedIndexChange displays the selected savings for the selected month
        Dim intSelectedCityIndex As Integer

        intSelectedCityIndex = cboMonths.SelectedIndex
        lblTotal.Text = "The electric savings for " & _strSavings(intSelectedCityIndex) & " is $" & _decBill(intSelectedCityIndex).ToString()
        btnDisplay.Visible = True
    End Sub

    Private Sub btnDisplay_Click(sender As Object, e As EventArgs) Handles btnDisplay.Click
        'the button click event call two sub methods to compute the average monthly savings and 
        'the most savings for the given month
        ComputeAverage()
        ComputeMaxSavings()

    End Sub

    Private Sub ComputeMaxSavings()
        'the ComputeMaxSavings sub methods determines the month with the most electric bill savings
        Dim intMonths As Integer
        Dim intLargestSavingsValue As Integer = 0
        Dim intIndexValue As Integer = 0
        Dim strYearValue As String = ""

        For Each intMonths In _decBill
            intLargestSavingsValue = Math.Max(intLargestSavingsValue, intMonths)
            If (intMonths >= intLargestSavingsValue) Then
                strYearValue = _strSavings(intIndexValue)
            End If
            intIndexValue += 1
        Next
        lblHighestMonth.Text = strYearValue & " had the most significant monthly savings"
    End Sub

    Private Sub ComputeAverage()
        'the ComputeAverage Sub method computes the average number of electric bill savings of Bill
        'in the years from 2000-2017
        Dim intCountYears As Integer
        Dim intYears As Integer = 0
        Dim decTotalBill As Decimal = 0
        Dim decAverageNumberOfBill As Decimal = 0D

        For Each intCountYears In _decBill
            decTotalBill += _decBill(intYears)
            intYears += 1
        Next
        decAverageNumberOfBill = decTotalBill / Convert.ToDecimal(_decBill.Length())
        lblAverage.Text = "The average monthly savings: " & decAverageNumberOfBill.ToString("C2")
    End Sub
End Class
