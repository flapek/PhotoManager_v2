using System.Windows;

namespace WpfCarouselDemo.WPFCarouselControl
{
    public interface ICarouselControl
    {
        void SelectElement(FrameworkElement element);
        double Rotate(double startXInScreenCoordinates, double endXInScreenCoordinates);
        void SelectFrontMostElement();
        void Rotate(double angleInDegrees);
        bool ShowRotation { get; set; }
    }
}
