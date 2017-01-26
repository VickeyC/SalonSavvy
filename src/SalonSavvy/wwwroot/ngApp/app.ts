namespace SalonSavvy {

    angular.module("SalonSavvy", ['ui.router', 'ngResource', 'ui.bootstrap', 'ngMaterial', 'ngMessages']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider,
        $mdThemingProvider: ng.material.IThemingProvider
    ) => {
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: SalonSavvy.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state('about', {
                url: '/about',
                templateUrl: '/ngApp/views/about.html',
                controller: SalonSavvy.Controllers.AboutController,
                controllerAs: 'controller'
            })
            .state('ourTeam', {
                url: '/ourTeam',
                templateUrl: '/ngApp/views/ourTeam.html',
                controller: SalonSavvy.Controllers.OurTeamController,
                controllerAs: 'controller'
            })
            //.state('products', {
            //    url: '/products',
            //    templateUrl: '/ngApp/views/products.html',
            //    controller: SalonSavvy.Controllers.ProductsController,
            //    controllerAs: 'controller'
            //})
            .state('location', {
                url: '/location',
                templateUrl: '/ngApp/views/location.html',
                controller: SalonSavvy.Controllers.LocationController,
                controllerAs: 'controller'
            })
            .state('appointmentType', {
                url: '/appointmentType',
                templateUrl: '/ngApp/views/appointmentType.html',
                controller: SalonSavvy.Controllers.AppointmentTypeController,
                controllerAs: 'controller'
            })
            .state('appointment', {
                url: '/appointment',
                templateUrl: '/ngApp/views/appointment.html',
                controller: SalonSavvy.Controllers.AppointmentController,
                controllerAs: 'controller'
            })
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            })
            .state('secret', {
                url: '/secret',
                templateUrl: '/ngApp/views/secret.html',
                controller: SalonSavvy.Controllers.SecretController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/ngApp/views/login.html',
                controller: SalonSavvy.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: SalonSavvy.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: SalonSavvy.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            })

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);

        // Define color palettes

        //$mdThemingProvider.definePalette('salonSavvyPrimary', {
        //    '50': 'ffebee',
        //    '100': 'ffcdd2',
        //    '200': 'ef9a9a',
        //    '300': 'e57373',
        //    '400': 'ef5350',
        //    '500': 'f44336',
        //    '600': 'e53935',
        //    '700': 'd32f2f',
        //    '800': 'c62828',
        //    '900': 'b71c1c',
        //    'A100': 'ff8a80',
        //    'A200': 'ff5252',
        //    'A400': 'ff1744',
        //    'A700': 'd50000',
        //    'contrastDefaultColor': 'light',    // whether, by default, text (contrast)
        //    // on this palette should be dark or light

        //    'contrastDarkColors': ['50', '100', //hues which contrast should be 'dark' by default
        //        '200', '300', '400', 'A100'],
        //    'contrastLightColors': undefined    // could also specify this if default was 'dark'
        //});


        $mdThemingProvider.theme('default')
            .primaryPalette('teal', {
                'default': '600',
                'hue-1': '500',
                'hue-2': '800',
                'hue-3': '900'
            })
            .accentPalette('purple', {
                'default': '600',
                'hue-1': '500',
                'hue-2': '800',
                'hue-3': '900'
            })
            .warnPalette('amber', {
                'default': '500',
                'hue-1': 'A200',
                'hue-2': '800',
                'hue-3': '900'
            });


        angular.module('SalonSavvy').factory('authInterceptor', (
            $q: ng.IQService,
            $window: ng.IWindowService,
            $location: ng.ILocationService
        ) =>
            ({
                request: function (config) {
                    config.headers = config.headers || {};
                    config.headers['X-Requested-With'] = 'XMLHttpRequest';
                    return config;
                },
                responseError: function (rejection) {
                    if (rejection.status === 401 || rejection.status === 403) {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            })
        );

        angular.module('SalonSavvy').config(function ($httpProvider) {
            $httpProvider.interceptors.push('authInterceptor');
        });

    }
    )
}