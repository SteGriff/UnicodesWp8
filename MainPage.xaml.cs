using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Unicodes.Resources;

namespace Unicodes
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void GetBytesButton_Click(object sender, RoutedEventArgs e)
        {
            var bytes = System.Text.Encoding.Unicode.GetBytes(InputBox.Text);
            
            string hexString = "";
            string decString = "";

            foreach (var b in bytes)
            {
                string hex = String.Format("{0:x} ", b);
                hexString += hex.ToUpper();

                int dec = (int)b;
                decString += dec.ToString() + " ";
            }

            OutputBox.Text = String.Format("HEX: {1}{0}DEC: {2}{0}", Environment.NewLine, hexString, decString);
        }

        private void GetTextFromHexButton_Click(object sender, RoutedEventArgs e)
        {
            BytesToText(true);
        }

        private void GetTextFromDecButton_Click(object sender, RoutedEventArgs e)
        {
            BytesToText(false);
        }

        private void BytesToText(bool isHex)
        {
            string[] parts;
            var bytes = new List<byte>();

            string input = BytesInputBox.Text.Trim();
            try
            {
                parts = input.Split(' ');
            }
            catch (Exception)
            {
                ByteToTextError("Make sure you enter are " + InputType(isHex) + " seperated by spaces");
                return;
            }

            foreach (string p in parts)
            {
                if (String.IsNullOrWhiteSpace(p))
                {
                    continue;
                }

                try
                {
                    byte b = isHex
                        ? Convert.ToByte("0x" + p)
                        : Byte.Parse(p);
                    
                    bytes.Add(b);
                }
                catch (Exception)
                {
                    ByteToTextError(p + " is not valid");
                    return;
                }

            }

            try
            {
                string text = System.Text.Encoding.Unicode.GetString(bytes.ToArray(), 0, bytes.Count);
                TextOutputBox.Text = text;
            }
            catch (Exception ex)
            {
                ByteToTextError("failed at the last step with: '" + ex.Message + "'");
                throw;
            }
            
        }

        private string InputType(bool hex)
        {
            return hex ? "hex bytes" : "decimal bytes";
        }

        private void ByteToTextError(string msg)
        {
            TextOutputBox.Text = "Can't convert: " + msg;
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}