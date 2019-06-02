Imports MySql.Data.EntityFrameworkCore.Extensions
Imports Microsoft.EntityFrameworkCore
Imports MySql.Data.MySqlClient
Imports System.Collections.Generic
Imports System.Linq
Imports System.ComponentModel.DataAnnotations
Imports Windows.Storage.Streams
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Microsoft.Azure.CognitiveServices.Vision.Face

'GettingintoHarvardand MIT
' The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x40


'Using the entity framework core to understand the whole topic
Public NotInheritable Class MainPage
    Inherits Page
    Dim qn As mycontext
    Dim sendingbyte As Byte()
    Dim receivingbyte As Byte()
    Dim masterhero As New List(Of finaldemons)
    Dim inforam As List(Of finaldemons) 'Changing the data
    Dim arat As Windows.Storage.StorageFile 'The file representing the image 


    'Just for artificial intelligence
    Dim kop As Stream

    Dim kam As New List(Of Models.DetectedFace
            )
    Dim vponx As New List(Of Models.FaceAttributeType
            )
    Dim ew As New ApiKeyServiceClientCredentials("35b6c6e481204452b20a84792f752e94")
    Dim vmoan As New FaceClient(ew)
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Student-oriented applications
        qn = New mycontext
        'Just for Aritificial intellignece
        vponx.Add(Models.FaceAttributeType.Emotion)
        vponx.Add(Models.FaceAttributeType.Age)
        vponx.Add(Models.FaceAttributeType.Gender)
        vponx.Add(Models.FaceAttributeType.Accessories)
        vponx.Add(Models.FaceAttributeType.Blur)
        vponx.Add(Models.FaceAttributeType.Exposure)
        vponx.Add(Models.FaceAttributeType.Smile)
        vmoan.Endpoint = "https://westcentralus.api.cognitive.microsoft.com"

    End Sub

    Public Sub creator()

    End Sub
    'A simple class to represent a type
    Public Class finaldemons


        Public Property StudentName As String
        Public Property StudentId As Integer
        Public Property BadFace As BitmapImage


        'This is the final model of the data

    End Class

    Sub loader()

        inforam = New List(Of finaldemons)
        Dim ewr As New finaldemons
        ewr.BadFace = New BitmapImage(New Uri("ms-appx:///Assets/StoreLogo.png"))
        ewr.StudentId = 34
        ewr.StudentName = "Kandel"


        inforam.Add(ewr)

        maingrid.DataContext = inforam
    End Sub






    Private Sub Se_Click(sender As Object, e As RoutedEventArgs) Handles Se.Click
        Protu1.IsActive = True
        'Just load data into the Grid
        Dim fpal = qn.masterdata.ToList
        Dim krom As New List(Of finaldemons)
        laodefficiently()



        Protu1.IsActive = False


        Tapu1.Text = "Fully Loaded"
    End Sub


    Private Sub Sendy_Click(sender As Object, e As RoutedEventArgs) Handles Sendy.Click

        Tapu1.Text = ""
        Dim vr As New studenti
        vr.stuname = FNAME.Text
        vr.stuid = CInt(ID.Text)
        vr.stuimage = sendingbyte
        qn.masterdata.Add(vr)
        qn.SaveChanges()

        'See how i used objects rather other things to connect to  the server



        'To show the refresheddata
        laodefficiently()










    End Sub

    Public Async Sub createbyte()
        Dim vr As New FileOpenPicker
        vr.FileTypeFilter.Add(".jpg")
        vr.FileTypeFilter.Add(".png")
        vr.FileTypeFilter.Add(".bmp")

        arat = Await vr.PickSingleFileAsync()

        Dim karlos As IRandomAccessStream = Await arat.OpenAsync(FileAccessMode.Read)
        Dim rfan As IBuffer = Await Windows.Storage.FileIO.ReadBufferAsync(arat)
        Dim kn As Byte() = rfan.ToArray
        sendingbyte = kn



    End Sub



    Public Async Sub laodefficiently()
        'Get the data in native format
        Dim masterhero As New List(Of finaldemons)
        Dim rawlist As New List(Of studenti)
        rawlist = qn.masterdata.ToList

        For Each asdf As studenti In rawlist
            Dim rendered As New finaldemons
            rendered.StudentId = asdf.stuid
            rendered.StudentName = asdf.stuname
            rendered.BadFace = Await returnimage(asdf.stuimage)
            masterhero.Add(rendered)
        Next

        'Finally show the data
        maingrid.DataContext = masterhero


    End Sub


    Public Async Function returnimage(vrom As Byte()) As Task(Of BitmapImage)
        Dim varnos As IBuffer = vrom.AsBuffer()
        Dim raers As Stream = varnos.AsStream()
        Dim fathom As IRandomAccessStream = raers.AsRandomAccessStream
        Dim qouras As New BitmapImage
        Await qouras.SetSourceAsync(fathom)
        'Finally the data is sorted
        Return qouras


    End Function





    Private Sub FILLU1_Click(sender As Object, e As RoutedEventArgs) Handles FILLU1.Click
        Tapu1.Text = ""
        createbyte()

    End Sub

    Private Async Sub AI_Click(sender As Object, e As RoutedEventArgs) Handles AI.Click

        kop = Await arat.OpenStreamForReadAsync()
        kam = Await vmoan.Face.DetectWithStreamAsync(kop, True, True, vponx, Nothing)

        'No new document has been created
        AIRESult.Text = kam.Item(0).FaceAttributes.Gender.ToString + "/" + kam.Item(0).FaceAttributes.Age.ToString + "/" + kam.Item(0).FaceAttributes.Emotion.Happiness.ToString

    End Sub
End Class
'Types of data to be taken care of












'The new context is to be applied
'So the datacontext is new
Public Class studenti
    Public Property stuname As String
    'Here will be the datatype of the enterprise system
    <Key> 'This is a primary ke
    Public Property stuid As Integer
    Public Property stuimage As Byte()
End Class
Public Class mycontext
    Inherits DbContext
    Public Property masterdata As DbSet(Of studenti)

    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        Dim vr As New MySqlConnectionStringBuilder
        vr.Server = "localhost"
        vr.Database = "trial1"
        vr.Password = "p1QrSaZ3"
        vr.UserID = "root"
        vr.SslMode = MySqlSslMode.Prefered
        vr.Port = "3306"
        optionsBuilder.UseMySQL(vr.GetConnectionString(True))
    End Sub
End Class

