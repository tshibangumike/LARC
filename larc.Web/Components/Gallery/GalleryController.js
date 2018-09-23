angular.module("larcApp")
    .controller("GalleryController",
    [
        "$scope", "$sce", "appQueries", "pictures",
        function ($scope, $sce, appQueries, pictures) {

            $scope.pictures = _.isNull(pictures) ? [] : pictures.data;
            $scope.months = [];

            $scope.GetPictureUrl = function (picture) {
                return $sce.trustAsResourceUrl("http://drive.google.com/uc?export=view&id=" + picture["GoogleDriveLink"]);
            };

            $scope.GetMonthsFromPictures = function () {
                var months = [];
                _.forEach($scope.pictures, function (picture) {
                    var dateString = picture["AddedOn"];
                    var dateParts = _.split(dateString, ":", 3);
                    var month = dateParts[0] + " " + dateParts[1];
                    if (!_.includes(months, month))
                        months.push(month);
                });
                return months;
            };

            $scope.GetPicturesByMonth = function (month) {
                return _.filter($scope.pictures, function (picture) {
                    var dateParts = $scope.GetDatePartsFromPicture(picture);
                    return dateParts[0] + " " + dateParts[1] == month
                });
            };

            $scope.GetDatePartsFromPicture = function (picture) {
                var dateString = picture["AddedOn"];
                return _.split(dateString, ":", 3);
            };

            var viewer = new Viewer(document.getElementById("images"));
            $scope.ShowImage = function (imageId) {
                new Viewer(document.getElementById(imageId), {
                    inline: false,
                    loading: true,
                    scalable: true,
                    viewed: function () {
                        viewer.zoomTo(1);
                    }
                });
            };

        }
    ]);



