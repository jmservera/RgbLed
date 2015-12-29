using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Pwm;
using Windows.UI;

namespace RgbLed
{
    public class RgbLed : IDisposable
    {
        PwmPin _redPin, _greenPin, _bluePin;
        PwmPin[] _pins;
        public RgbLed(PwmPin redPin, PwmPin greenPin, PwmPin bluePin)
        {
            _redPin = redPin;
            _greenPin = greenPin;
            _bluePin = bluePin;
            _pins = new[] { _redPin, _greenPin, _bluePin };
        }

        public void On()
        {
            for (int i = 0; i < _pins.Length; i++)
            {
                if (!_pins[i].IsStarted)
                    _pins[i].Start();
            }
        }

        public void Off()
        {
            for (int i = 0; i < _pins.Length; i++)
            {
                if (_pins[i].IsStarted)
                    _pins[i].Stop();
            }
        }

        private Color _color;

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                updateLed();
            }
        }


        private void updateLed()
        {
            double[] colorValue = new double[] { Color.R, Color.G, Color.B };
            for (int i = 0; i < colorValue.Length; i++)
            {
                colorValue[i] /= 255d;
                _pins[i].SetActiveDutyCyclePercentage(colorValue[i]);
            }
        }

        public void Dispose()
        {
            if (_pins != null)
            {
                for (int i = 0; i < _pins.Length; i++)
                {
                    if (_pins[i].IsStarted)
                    {
                        _pins[i].Stop();
                    }
                    _pins[i].Dispose();
                }
                _pins = null;
            }
        }
    }
}
