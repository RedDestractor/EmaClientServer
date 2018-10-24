using PointsClient.Helpers.Interface;
using PointsClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PointsClient
{
    public class PointsManager
    {
        public List<Point> Points { get; private set; } = new List<Point>();

        private IPointsHandler _pointsHandler;
        private DialogServiceInterface _dialogService;

        public PointsManager(IPointsHandler pointsHandler, DialogServiceInterface dialogService)
        {
            _pointsHandler = pointsHandler;
            _dialogService = dialogService;
        }

        public async Task<bool> GetAllPoints()
        {
            IList<Point> points = null;

            try
            {
                 points = await _pointsHandler.GetAllPoints();
            }
            catch (Exception e)
            {
                _dialogService.ShowMessageBox(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (points != null)
                Points = points.ToList();

            return true;
        }

        public async Task<bool> SavePoints(List<Point> points)
        {
            bool result;
            try
            {
                result = await _pointsHandler.SavePoints(points);
            }
            catch (Exception e)
            {
                _dialogService.ShowMessageBox(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (result == false)
                _dialogService.ShowMessageBox("Saving of points had unexpected result: Return node was null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            return true;
        }
    }
}
