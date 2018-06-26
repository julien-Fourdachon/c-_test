using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using Newtonsoft.Json;



namespace WpfApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        LocationConverter locConverter = new LocationConverter();
        string toto;



        public MainWindow()
        {
            InitializeComponent();
            myMap.Focus();




        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myMap.Children.Clear();
            LinesBus lineInfo = new LinesBus();
            BusStop busstop = new BusStop();
            Api api = new Api();

            var lat = TxtLat.Text;
            var lng = TxtLon.Text;
            var ryn = Rayon.Text;



            String getApiBusStop = api.GetApi("http://data.metromobilite.fr/api/linesNear/json?x=" + lng + "&y=" + lat + "&dist=" + ryn + "&details=true");
            String getApiBusLines = api.GetApi("http://data.metromobilite.fr/api/routers/default/index/routes");


            List<BusStop> busStops = JsonConvert.DeserializeObject<List<BusStop>>(getApiBusStop);
            List<BusStop> names = busStops.GroupBy(y => y.Name).Select(x => x.First()).ToList();
            List<LinesBus> linedetail = JsonConvert.DeserializeObject<List<LinesBus>>(getApiBusLines);


            



            foreach (BusStop name in names)
            {

                foreach (BusStop bustop in busStops)
                {
                    if (name.Name.Equals(bustop.Name))
                    {
                        IEnumerable<string> newLines = name.Lines.Union(bustop.Lines);
                        name.GetType().GetProperty("Lines").SetValue(name, newLines);
                    }
                }

            }
            foreach (BusStop name in names)
            {
                toto = "";
                //Lignes.Items.Add(name.Name);

                

                foreach (string lines in name.Lines)
                {
                    toto += "\n" + lines;
                 
                    foreach (LinesBus lineColor in linedetail)
                    {
                        if (lineColor.Id.Equals(lines))
                        {
                            lineInfo = lineColor;
                        }
                       

                    }
                    MapLayer imageLayer = new MapLayer();
                    Image image = new Image();
                    image.Height = 150;
                    //Define the URI location of the image
                    BitmapImage myBitmapImage = new BitmapImage();
                    myBitmapImage.BeginInit();
                    myBitmapImage.UriSource = new Uri("https://orig00.deviantart.net/4dfb/f/2014/090/6/1/pikachu___01_by_mighty355-d7cdjy7.png");
                    myBitmapImage.DecodePixelHeight = 50;
                    myBitmapImage.EndInit();
                    image.Source = myBitmapImage;
                    image.Opacity = 1;
                    image.Stretch = System.Windows.Media.Stretch.None;


                    Location location = new Location(name.Lat, name.Lon);
                    PositionOrigin position = PositionOrigin.Center;
                    imageLayer.AddChild(image, location, position);
                    ToolTipService.SetToolTip(imageLayer, $"Nom de l'arrêt: {name.Name}\n Lignes: {toto}");
                    myMap.Children.Add(imageLayer);


     


                }

            }
            // Lignes.Items.Add($"\nMode de transport: {lineInfo.Mode}\nNom Complet : {lineInfo.LongName}");


            //Lignes.Items.Add($"\n");

        }
    }
}