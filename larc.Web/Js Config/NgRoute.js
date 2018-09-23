
angular.module("larcApp")
    .config([
        "$stateProvider", "$urlRouterProvider", "$httpProvider",
        function ($stateProvider, $urlRouterProvider, $httpProvider) {

            //$httpProvider.interceptors.push("httpLoader");
            $urlRouterProvider.otherwise("/Home");

            var states = [
                larc.Routes.SetRoutes("Home"),
                larc.Routes.SetRoutes("Podcasts"),
                larc.Routes.SetRoutes("Podcast"),
                larc.Routes.SetRoutes("Events"),
                larc.Routes.SetRoutes("Bible"),
                larc.Routes.SetRoutes("Gallery"),
                larc.Routes.SetRoutes("ContactUs"),
                larc.Routes.SetRoutes("Admin"),
            ];

            states.forEach(function (state) {
                $stateProvider.state(state);
            });

        }
    ]);
