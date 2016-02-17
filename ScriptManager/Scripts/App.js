var sampleApp = angular.module('sampleApp', ['smart-table']);

var httpUrl = 'http://localhost:59899';

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
        otherwise({
            redirectTo: '/ManageScripts'
        });
  }]);

sampleApp.controller('ScriptDetailController', function($scope, $routeParams, $http)
{
    $scope.Id = $routeParams.scriptId;
    
    function LoadScript($scope, $http)
    {
        $http.get(httpUrl + '/GetScriptById?scriptId=' + $scope.Id).
        success(function (data) {
            alert(data);
            }            
        );
    }
    LoadScript($scope, $http);
}
);

sampleApp.controller('ScriptController', function ($scope,$http) {
    var id = 0;

    function LoadScripts($scope, $http) {
        $http.get(httpUrl + '/api/script/?').
            success(function (data) {
                
                for (id; id < data.length; id++)
                {
                    $scope.rowCollection.push(data[id]);
                }
            });
    };
    LoadScripts($scope, $http);   

});


sampleApp.controller('ReleasesController', function ($scope) {

    $scope.message = 'This is releases controller.';

});