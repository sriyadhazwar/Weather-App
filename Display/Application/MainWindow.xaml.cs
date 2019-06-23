namespace Display
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new DisplayPage();
        }
    }
}