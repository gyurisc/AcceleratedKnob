using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Devices.Sensors;
using System.ComponentModel;

namespace AcceleratedKnob_WP7
{
	public partial class AcceleratedKnob : UserControl
	{
        Accelerometer accelerometer;

        // rotations    
        public double darkImageRotation = 0;
        public double leftImageRotation = 0;
        public double rightImageRotation = 0; 

		public AcceleratedKnob()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.IsInDesignTool)
            {
                accelerometer = new Accelerometer();

                accelerometer.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(acc_CurrentValueChanged);
                accelerometer.Start();                
            }
        }

        void acc_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => updateMyScreen(e.SensorReading));
        }

        void updateMyScreen(AccelerometerReading e)
        {
            Microsoft.Xna.Framework.Vector3 v = e.Acceleration;

            darkImageRotation = (darkImageRotation * 0.7) + v.X * 0.3;
            leftImageRotation = (leftImageRotation * 0.7) + (v.Y - darkImageRotation) * 0.3;
            rightImageRotation = (rightImageRotation * 0.7) + (v.Z - leftImageRotation) * 0.3;


            DarkImageProjection.RotationZ = darkImageRotation * 100;
            RightImageProjection.RotationZ = rightImageRotation * 100;
            LeftImageProjection.RotationZ = leftImageRotation * 100;
        }
	}
}