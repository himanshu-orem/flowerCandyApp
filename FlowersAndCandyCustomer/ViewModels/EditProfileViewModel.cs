using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using Plugin.ImageResizer;
using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class EditProfileViewModel : INotifyPropertyChanged
    {
        public byte[] _profileImage;
        public byte[] _civilImage;
        public byte[] _vehicleCopyImage;
        public byte[] _vehicleImage;

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber"));
            }
        }

        private ImageSource _profileImageSource;
        public ImageSource ProfileImageSource
        {
            get
            {
                return _profileImageSource;
            }
            set
            {
                _profileImageSource = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProfileImageSource"));
            }
        }

        private ImageSource _civilRegisterImageSource;
        public ImageSource CivilRegisterImageSource
        {
            get
            {
                return _civilRegisterImageSource;
            }
            set
            {
                _civilRegisterImageSource = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CivilRegisterImageSource"));
            }
        }

        private ImageSource _vehicleCopyImageSource;
        public ImageSource VehicleCopyImageSource
        {
            get
            {
                return _vehicleCopyImageSource;
            }
            set
            {
                _vehicleCopyImageSource = value;
                PropertyChanged(this, new PropertyChangedEventArgs("VehicleCopyImageSource"));
            }
        }

        private ImageSource _vehicleImageSource;
        public ImageSource VehicleImageSource
        {
            get
            {
                return _vehicleImageSource;
            }
            set
            {
                _vehicleImageSource = value;
                PropertyChanged(this, new PropertyChangedEventArgs("VehicleImageSource"));
            }
        }

        public EditProfileViewModel()
        {
            _name = "John Smith";
            _email = "test@test.com";
            _phoneNumber = "9592528057";
            _profileImageSource = "profile.jpg";
            _vehicleCopyImageSource = "add_photo.png";
            _civilRegisterImageSource = "add_photo.png";
            _vehicleImageSource = "add_photo.png";
        }

        /// <summary>
        /// This tapped event will be used for profile picture choose.
        /// </summary>
        public Command profilePictureCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    try
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet(AppResources.choose_image, AppResources.cancel, null,
                           AppResources.take_picture, AppResources.choose_from_gallery);

                        if (action == AppResources.take_picture)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            if (Device.RuntimePlatform == "iOS")
                            {
                                await Task.Delay(1000);
                            }
                            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.jpg"
                            });
                            if (file == null)
                                return;
                            using (var memoryStream = new MemoryStream())
                            {
                                file.GetStream().CopyTo(memoryStream);
                                var myfile = memoryStream.ToArray();
                                _profileImage = myfile;
                              //  _profileImage = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);


                                file.Dispose();
                            }
                            ProfileImageSource = ImageSource.FromStream(() => new MemoryStream(_profileImage));
                        }
                        else if (action == AppResources.choose_from_gallery)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            var file = await CrossMedia.Current.PickPhotoAsync();
                            if (file != null)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    var myfile = memoryStream.ToArray();
                                    _profileImage = myfile;
                                   // _profileImage = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);

                                    file.Dispose();
                                }
                                ProfileImageSource = ImageSource.FromStream(() => new MemoryStream(_profileImage));
                            }
                        }
                        else
                        {
                            return;
                        }

                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }


        public Command vehicleCopyPictureCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    try
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet(AppResources.choose_image, AppResources.cancel, null,
                           AppResources.take_picture, AppResources.choose_from_gallery);

                        if (action == AppResources.take_picture)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            if (Device.RuntimePlatform == "iOS")
                            {
                                await Task.Delay(1000);
                            }
                            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.jpg"
                            });
                            if (file == null)
                                return;
                            using (var memoryStream = new MemoryStream())
                            {
                                file.GetStream().CopyTo(memoryStream);
                                var myfile = memoryStream.ToArray();
                                _vehicleCopyImage = myfile;
                                file.Dispose();
                            }
                            VehicleCopyImageSource = ImageSource.FromStream(() => new MemoryStream(_vehicleCopyImage));
                        }
                        else if (action == AppResources.choose_from_gallery)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            var file = await CrossMedia.Current.PickPhotoAsync();
                            if (file != null)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    var myfile = memoryStream.ToArray();
                                    _vehicleCopyImage = myfile;
                                    file.Dispose();
                                }
                                VehicleCopyImageSource = ImageSource.FromStream(() => new MemoryStream(_vehicleCopyImage));
                            }
                        }
                        else
                        {
                            return;
                        }

                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }


        public Command vehiclePictureCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    try
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet(AppResources.choose_image, AppResources.cancel, null,
                           AppResources.take_picture, AppResources.choose_from_gallery);

                        if (action == AppResources.take_picture)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            if (Device.RuntimePlatform == "iOS")
                            {
                                await Task.Delay(1000);
                            }
                            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.jpg"
                            });
                            if (file == null)
                                return;
                            using (var memoryStream = new MemoryStream())
                            {
                                file.GetStream().CopyTo(memoryStream);
                                var myfile = memoryStream.ToArray();
                                _vehicleImage = myfile;
                                file.Dispose();
                            }
                            VehicleImageSource = ImageSource.FromStream(() => new MemoryStream(_vehicleImage));
                        }
                        else if (action == AppResources.choose_from_gallery)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            var file = await CrossMedia.Current.PickPhotoAsync();
                            if (file != null)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    var myfile = memoryStream.ToArray();
                                    _vehicleImage = myfile;
                                    file.Dispose();
                                }
                                VehicleImageSource = ImageSource.FromStream(() => new MemoryStream(_vehicleImage));
                            }
                        }
                        else
                        {
                            return;
                        }

                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }


        public Command civilCopyPictureCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    try
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet(AppResources.choose_image, AppResources.cancel, null,
                           AppResources.take_picture, AppResources.choose_from_gallery);

                        if (action == AppResources.take_picture)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            if (Device.RuntimePlatform == "iOS")
                            {
                                await Task.Delay(1000);
                            }
                            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.jpg"
                            });
                            if (file == null)
                                return;
                            using (var memoryStream = new MemoryStream())
                            {
                                file.GetStream().CopyTo(memoryStream);
                                var myfile = memoryStream.ToArray();
                                _civilImage = myfile;
                                file.Dispose();
                            }
                            CivilRegisterImageSource = ImageSource.FromStream(() => new MemoryStream(_civilImage));
                        }
                        else if (action == AppResources.choose_from_gallery)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            var file = await CrossMedia.Current.PickPhotoAsync();
                            if (file != null)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    var myfile = memoryStream.ToArray();
                                    _civilImage = myfile;
                                    file.Dispose();
                                }
                                CivilRegisterImageSource = ImageSource.FromStream(() => new MemoryStream(_civilImage));
                            }
                        }
                        else
                        {
                            return;
                        }

                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }

        public Command editProfileCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var returnMessage = CheckValidations();
                    if (!string.IsNullOrEmpty(returnMessage))
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(returnMessage));
                        await Task.Delay(1000);
                        await App.Current.MainPage.Navigation.PopPopupAsync();
                        return;
                    }
                });
            }
        }

        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Name))
            {
                msg += AppResources.enter_name_validation + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(Email))
            {
                msg += AppResources.enter_email_validation + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(Email))
            {
                if (!CommonLib.CheckValidEmail(Email))
                {
                    msg += AppResources.enter_valid_email_validation + Environment.NewLine;
                }
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                msg += AppResources.enter_phone_number_validation + Environment.NewLine;
            }
            return msg;
        }

        public Command BackImgTapped
        {
            get
            {
                return new Command((e) =>
                {
                    App.Current.MainPage.Navigation.PopAsync();
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
