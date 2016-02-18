var sampleApp = angular.module('sampleApp', ['smart-table']);

var httpUrl = '';

sampleApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/ManageScripts', {
            templateUrl: 'Screens/ManageScripts.html',
            controller: 'ScriptController'
        }).
        when('/ManageReleases', {
            templateUrl: 'Screens/ManageReleases.html',
            controller: 'ReleasesController'
        }).
        when('/ScriptDetail/:scriptId', {
            templateUrl: 'Screens/ScriptDetail.html',
            controller: 'ScriptDetailController'
        }).
        when('/ReportAMistake', {
            templateUrl: 'Screens/Flags.html',
            controller: 'FlagsController'
        }).
        when('/FlagDetail/:flagId', {
            templateUrl: 'Screens/ReportNewIssue.html',
            controller: 'ReportAnIssueController'
        }).
        otherwise({
            redirectTo: '/ManageScripts'
        });
  }]);

sampleApp.controller('ScriptController', function ($scope, $http) {
    var id = 0;
    $scope.rowCollection = [];
    function LoadScripts($scope, $http) {
        $http.get(httpUrl + '/api/script').
            success(function (data) {
               
                for (id; id < data.length; id++) {
                    $scope.rowCollection.push(data[id]);
                }
            });
    };
    LoadScripts($scope, $http);

});


sampleApp.controller('ReleasesController', function ($scope) {

    $scope.message = 'This is releases controller.';

});

