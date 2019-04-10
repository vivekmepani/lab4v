Option Strict On

''' <summary>
''' Author Name:    Darshan patel
''' Project Name:   Car Inventory
''' Date:           15 March, 2019
''' Description     This is a lab 4 for NETD class, keeps record for the cars
''' </summary>
''' 
Public Class frmCarInventory

    Private carList As New SortedList   ' collection of all the carList in the list
    Private currentCarIdentificationNumber As String = String.Empty ' current selected car identification number
    Private editMode As Boolean = False

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()   'End the application
    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        Dim car As Car  ' declare a Car class
        Dim carItem As ListViewItem     ' declare a ListViewItem class

        ' validate the data in the form
        If IsValidInput() = True Then

            ' set the edit flag to true
            editMode = True
            lblResult.Text = "Data entered correctly"


            ' if the current car identification number has a no value
            ' then this is not an existing item from the listview
            If currentCarIdentificationNumber.Trim.Length = 0 Then

                ' create a new car object using the parameterized constructor
                car = New Car(cmbMake.Text, txtModel.Text, cmbYear.Text, txtPrice.Text, chkNew.Checked)

                ' add the car to the carList collection
                ' using the identoification number as the key
                ' which will make the car object easier to
                ' find in the carList collection later
                carList.Add(car.IdentificationNumber.ToString(), car)
            Else
                ' if the current car identification number has a value
                ' then the user has selected something from the list view
                ' so the data in the car object in the carList collection
                ' must be updated

                ' so get the car from the car collection
                ' using the selected key
                car = CType(carList.Item(currentCarIdentificationNumber), Car)

                ' update the data in the specific object
                ' from the controls
                car.CarPropertyMake = cmbMake.Text
                car.CarModelProperty = txtModel.Text
                car.CarPropertyPrice = txtPrice.Text
                car.CarYearPropery = cmbYear.Text
                car.IsCarNew = chkNew.Checked

            End If

            ' clear the items from the listview control
            lvwCar.Items.Clear()

            For Each carEntry As DictionaryEntry In carList

                carItem = New ListViewItem()

                car = CType(carEntry.Value, Car)

                carItem.Checked = car.IsCarNew
                carItem.SubItems.Add(car.IdentificationNumber.ToString())
                carItem.SubItems.Add(car.CarPropertyMake)
                carItem.SubItems.Add(car.CarModelProperty)
                carItem.SubItems.Add(car.CarYearPropery)
                carItem.SubItems.Add(car.CarPropertyPrice)

                lvwCar.Items.Add(carItem)

            Next carEntry

            Reset()
            editMode = False

        End If

    End Sub

    Private Sub Reset()


        txtModel.Text = String.Empty
        txtPrice.Text = String.Empty
        cmbYear.SelectedIndex = -1
        cmbMake.SelectedIndex = -1
        chkNew.Checked = False

        currentCarIdentificationNumber = String.Empty
        lblResult.Text = String.Empty

    End Sub

    Private Function IsValidInput() As Boolean

        Dim returnValue As Boolean = True
        Dim outputMessage As String = String.Empty
        Dim price As Decimal = 0.0D

        ' check if the title has been selected
        If cmbMake.SelectedIndex = -1 Then

            ' If not set the error message
            outputMessage += "Please enter the car's make" & vbCrLf

            ' And, set the return value to false
            returnValue = False

        End If

        If cmbYear.SelectedIndex = -1 Then

            ' If not set the error message
            outputMessage += "Please enter car's year" & vbCrLf

            ' And, set the return value to false
            returnValue = False
        End If


        ' check if the first name has been entered
        If txtModel.Text.Trim.Length = 0 Then

            ' If not set the error message
            outputMessage += "Please enter Model of the car" & vbCrLf

            ' And, set the return value to false
            returnValue = False

        End If

        ' check if the first name has been entered
        If txtPrice.Text.Trim.Length = 0 Then

            ' If not set the error message
            outputMessage += "Please enter price of the Car" & vbCrLf

            ' And, set the return value to false
            returnValue = False

        End If

        If Not (Decimal.TryParse(txtPrice.Text, price)) Then

            ' If not set the error message
            outputMessage += "Please enter Valid car price" & vbCrLf

            ' And, set the return value to false
            returnValue = False


        End If

        If (Decimal.TryParse(txtPrice.Text, price)) Then

            If price < 0 Then

                outputMessage += "Only positive value are allowed for the price of the car" & vbCrLf

                ' And, set the return value to false
                returnValue = False
            End If

        End If




        ' check to see if any value
        ' did not validate
        If returnValue = False Then

            ' show the message(s) to the user
            lblResult.Text = "ERRORS" & vbCrLf & outputMessage

        End If

        ' return the boolean value
        ' true if it passed validation
        ' false if it did not pass validation
        Return returnValue

    End Function

    Private Sub lvwCar_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvwCar.ItemCheck

        ' if it is not in edit mode
        If editMode = False Then

            ' the new value to the current value
            ' so it cannot be set in the listview by the user
            e.NewValue = e.CurrentValue

        End If

    End Sub

    Private Sub lvwCar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwCar.SelectedIndexChanged
        Const identificationSubItemIndex As Integer = 1

        ' Get the car identification number 
        currentCarIdentificationNumber = lvwCar.Items(lvwCar.FocusedItem.Index).SubItems(identificationSubItemIndex).Text

        ' Use the car identification number to get the car from the collection object
        Dim car As Car = CType(carList.Item(currentCarIdentificationNumber), Car)

        ' set the controls on the form
        txtModel.Text = car.CarModelProperty               ' get the first name and set the text box
        txtPrice.Text = car.CarPropertyPrice                 ' get the last name and set the text box
        cmbMake.Text = car.CarPropertyMake
        cmbYear.Text = car.CarYearPropery

        ' get the title and set the combo box
        chkNew.Checked = car.IsCarNew ' get the very important person status and set the combo box


    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
End Class
