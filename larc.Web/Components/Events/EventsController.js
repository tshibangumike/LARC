angular.module("larcApp")
    .controller("EventsController",
    [
        "$scope", "appQueries", "events",
        function ($scope, appQueries, events) {
            $scope.events = _.isNull(events) ? [] : events.data;
            //$scope.events = [
            //    {
            //        title: "Reunion des papas",
            //        start: new Date('2018-09-19 20:30'),
            //        end: new Date('2018-09-19 22:30')
            //    },
            //    {
            //        title: "Sunday Service - ",
            //        start: new Date('2018-09-23 13:30'),
            //        end: new Date('2018-09-23 15:45')
                    
            //    }
            //];
        }
    ]);