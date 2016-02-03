# Controlling the RGB Led intensities in RPI2 with IoT Core and the PWM library

A Windows IoT Core RBG Led sample using the [PWM library](https://github.com/ms-iot/BusProviders/tree/develop/PWM)

When you clone this repository remember to clone the submodules as well:

```
git clone --recursive https://github.com/jmservera/RgbLed.git
```


## The RGB values
For controlling the intensity of the RGB values in the GPIO pins, a Pulse Width Modulation (PWM) is needed.
We can use a PWM circuit for it or simulate it via software.

For this example we will use the software modulation found in the [PWM library](https://github.com/ms-iot/BusProviders/tree/develop/PWM)

## Wiring
Experimenting right now with the wiring like here:
![RGB Led wiring](https://raw.githubusercontent.com/geerlingguy/raspberry-pi-dramble/master/images/rgb-led-wiring.jpg)

