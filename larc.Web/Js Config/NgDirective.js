angular.module("larcApp")
    .directive('convertToNumber', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function (val) {
                    return val != null ? parseInt(val, 10) : null;
                });
                ngModel.$formatters.push(function (val) {
                    return val != null ? '' + val : null;
                });
            }
        };
    })
    .directive("limitTo", function () {
        return {
            restrict: "A",
            link: function (scope, elem, attrs) {
                var limit = parseInt(attrs.limitTo);
                angular.element(elem).on("keypress",
                    function (e) {
                        if (this.value.length == limit) e.preventDefault();
                    });
            }
        }
    })
    .directive('stopPropagation', function () {
        return {
            restrict: 'A',
            link: function (scope, element) {
                element.bind('click', function (e) {
                    return false;
                });
            }
        };
    })
    .filter('jsonDate', ['$filter', function ($filter) {
        return function (input, format) {
            return (input)
                ? $filter('date')(parseInt(input.substr(6)), format)
                : '';
        };
    }]);
