angular.module("larcApp")
    .controller("PodcastsController",
    [
        "$scope", "$filter", "$sce", "appQueries", "podcasts",
        function ($scope, $filter, $sce, appQueries, podcasts) {
            $scope.podcasts = _.isNull(podcasts) ? [] : podcasts.data;
            $scope.months = [{ name: "August" }, { name: "September" }, { name: "October" }];
            $scope.GetPodcastsByMonth = function (month) {
                return _.filter($scope.podcasts, function (podcast) {
                    return larc.Functions.GetMonthFromDate($filter, podcast["StartTime"], "MMMM YYYY") == month;
                })
            };
            $scope.GetMonthFromDate = function (date) {
                return larc.Functions.GetMonthFromDate($filter, date, "MMMM YYYY");
            };
            $scope.GetMonthsFromPodcastsDate = function () {
                var months = [];
                _.forEach($scope.podcasts, function (podcast) {
                    var month = larc.Functions.GetMonthFromDate($filter, podcast["StartTime"], "MMMM YYYY");
                    if (!_.includes(months, month))
                        months.push(month);
                    //var dateString = picture["StartTime"];
                    //var dateParts = _.split(dateString, ":", 3);
                    //var month = dateParts[0] + " " + dateParts[1];
                    //if (!_.includes(months, month))
                    //    months.push(month);
                });
                return months;
            };
            $scope.ListenToPodcast = function (podcast) {
                appQueries.NavigateTo("Podcast", { podcastid: podcast["GoogleAttachmentId"] });
            };
            $scope.GetDatePartsFromPicture = function (picture) {
                var dateString = picture["StartTime"];
                return _.split(dateString, ":", 3);
            };
            $scope.GetPodcastLink = function (podcast) {
                var url = "https://drive.google.com/file/d/" + podcast["GoogleAttachmentId"] + "/preview";
                return $sce.trustAsResourceUrl(url);
            }
        }
    ])
    .controller("PodcastController",
    [
        "$scope", "$sce", "appQueries", "podcast",
        function ($scope, $sce, appQueries, podcast) {
            $scope.podcast = _.isNull(podcast) ? {} : podcast.data;
            $scope.Url = $sce.trustAsResourceUrl($scope.podcast.GoogleDriveLink);
            $scope.BackToPodcasts = function () {
                appQueries.NavigateTo("Podcasts");
            };
        }
    ]);