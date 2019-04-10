Option Strict On

''' <summary>
''' Author Name:    Vivek Mepani
''' Project Name:   Car Inventory LAB 4 NETD
''' Description     Lab 4 for Net Development 
''' </summary>
''' 
Public Class frmCarInventory

    Private carSrtedLists As New SortedList
    Private currentIdOfCar As String = String.Empty
    Private editTrueFalse As Boolean = False



    Private Sub Reset()


        txtModel.Text = String.Empty
        txtPrice.Text = String.Empty
        cmbYear.SelectedIndex = -1
        cmbMake.SelectedIndex = -1
        chkNew.Checked = False

        currentIdOfCar = String.Empty
        lblResult.Text = String.Empty

    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        Dim car As Car
        Dim carItem As ListViewItem

        If IsValidInput() = True Then
            editTrueFalse = True
            lblResult.Text = "Good!  Data is correctly entered"

            If currentIdOfCar.Trim.Length = 0 Then

                car = New Car(cmbMake.Text, txtModel.Text, cmbYear.Text, txtPrice.Text, chkNew.Checked)
                carSrtedLists.Add(car.IdentificationNumber.ToString(), car)

            Else
                car.IsCarNew = chkNew.Checked
                car = CType(carSrtedLists.Item(currentIdOfCar), Car)

                car.CarModelProperty = txtModel.Text
                car.CarPropertyPrice = txtPrice.Text
                car.CarYearPropery = cmbYear.Text
                car.CarPropertyMake = cmbMake.Text


            End If
            lvwCar.Items.Clear()

            For Each carEntry As DictionaryEntry In carSrtedLists

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

            editTrueFalse = False
            Reset()


        End If

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub



    Private Function IsValidInput() As Boolean

        Dim returnTorF As Boolean = True
        Dim outString As String = String.Empty
        Dim costpriceDollarsformat1 As Decimal = 0.0D

        If cmbMake.SelectedIndex = -1 Then

            outString += "Please enter the car's make" & vbCrLf

            returnTorF = False

        End If

        If cmbYear.SelectedIndex = -1 Then

            outString += "Please enter car's year" & vbCrLf

            returnTorF = False
        End If


        If txtModel.Text.Trim.Length = 0 Then

            outString += "Please enter Model of the car" & vbCrLf

            returnTorF = False

        End If

        If txtPrice.Text.Trim.Length = 0 Then

            outString += "Please enter price of the Car" & vbCrLf

            returnTorF = False

        End If

        If Not (Decimal.TryParse(txtPrice.Text, costpriceDollarsformat1)) Then

            outString += "Please enter Valid car of the car" & vbCrLf

            returnTorF = False


        End If

        If (Decimal.TryParse(txtPrice.Text, costpriceDollarsformat1)) Then

            If costpriceDollarsformat1 < 0 Then

                outString += "Only positive value are allowed for the price of the car" & vbCrLf

                returnTorF = False
            End If

        End If

        If returnTorF = False Then

            lblResult.Text = "Errors: " & vbCrLf & outString

        End If

        Return returnTorF

    End Function

    Private Sub lvwCar_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvwCar.ItemCheck

        If editTrueFalse = False Then

            e.NewValue = e.CurrentValue

        End If

    End Sub

    Private Sub lvwCar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwCar.SelectedIndexChanged
        Const identificationSubItemIndex As Integer = 1

        currentIdOfCar = lvwCar.Items(lvwCar.FocusedItem.Index).SubItems(identificationSubItemIndex).Text

        Dim car As Car = CType(carSrtedLists.Item(currentIdOfCar), Car)

        txtModel.Text = car.CarModelProperty
        txtPrice.Text = car.CarPropertyPrice
        cmbMake.Text = car.CarPropertyMake
        cmbYear.Text = car.CarYearPropery

        chkNew.Checked = car.IsCarNew


    End Sub



    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
