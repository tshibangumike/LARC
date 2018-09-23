angular.module("larcApp")
    .controller("BaseController",
    [
        "$scope", "appQueries",
        function ($scope, appQueries) {
            $scope.currentState = appQueries.GetCurrentState();
        }
    ]);