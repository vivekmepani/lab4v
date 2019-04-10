
''' Here is the class for the car which store all the value

Option Strict On

Public Class Car

    Private Shared carCount As Integer
    Private carIdNumber As Integer
    Private carMake As String = String.Empty
    Private carModel As String = String.Empty
    Private carYear As String = String.Empty
    Private carPrice As String = String.Empty
    Private chkNewCar As Boolean = False
    Private priceCarinDecimal As Decimal = 0.0D


    Public Sub New()
        carCount += 1
        carIdNumber = carCount
    End Sub

    Public Sub New(make As String, model As String, year As String, price As String, isNew As Boolean)
        Me.New()

        carMake = make
        carModel = model
        carYear = year
        carPrice = price
        chkNewCar = isNew
    End Sub


    Public ReadOnly Property IdentificationNumber() As Integer
        Get
            Return carIdNumber
        End Get
    End Property

    Public Property IsCarNew() As Boolean
        Get
            Return chkNewCar
        End Get
        Set(ByVal value As Boolean)
            chkNewCar = value
        End Set
    End Property

    ''''''''''''''''''''''''
    Public Property CarPropertyMake() As String
        Get
            Return carMake
        End Get
        Set(ByVal value As String)
            carMake = value
        End Set
    End Property

    Public Property CarPropertyPrice() As String
        Get
            Decimal.TryParse(carPrice, priceCarinDecimal)
            priceCarinDecimal = Math.Round(priceCarinDecimal, 2)
            carPrice = FormatCurrency(priceCarinDecimal.ToString())
            Return carPrice
        End Get
        Set(ByVal value As String)

            carPrice = value
        End Set
    End Property

    Public Property CarModelProperty() As String
        Get
            Return carModel
        End Get
        Set(ByVal value As String)
            carModel = value
        End Set
    End Property

    Public Property CarYearPropery() As String
        Get
            Return carYear
        End Get
        Set(ByVal value As String)
            carYear = value
        End Set
    End Property

End Class
