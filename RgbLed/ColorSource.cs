using GalaSoft.MvvmLight;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace RgbLed
{
    public class ColorSource : ObservableObject
    {
        double r, g, b;
        Color color;
        public double R
        {
            get { return r; }
            set { Set(ref r, value); updateColor(); }
        }
        public double G
        {
            get { return g; }
            set { Set(ref g, value); updateColor(); }
        }
        public double B
        {
            get { return b; }
            set { Set(ref b, value); updateColor(); }
        }

        bool updating;
        void updateColor()
        {
            if (!updating)
            {
                updating = true;
                try
                {
                    Color = Color.FromArgb(255, (byte)R, (byte)G, (byte)B);
                }
                finally
                {
                    updating = false;
                }
            }
        }
        public Color Color
        {
            get { return color; }
            set
            {
                Set(ref color, value);
                if (!updating)
                {
                    updateRGB();
                }
                base.RaisePropertyChanged("ColorBrush");
            }
        }

        private void updateRGB()
        {
            if (!updating)
            {
                updating = true;
                try
                {
                    this.R = this.Color.R;
                    this.G = this.Color.G;
                    this.B = this.Color.B;
                }
                finally
                {
                    updating = false;
                }
            }
        }

        public SolidColorBrush ColorBrush
        {
            get { return new SolidColorBrush(Color); }
        }
    }

}
