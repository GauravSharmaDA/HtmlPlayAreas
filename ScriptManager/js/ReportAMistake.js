
sampleApp.controller('FlagsController', function ($scope, $http) {
    $scope.flags = [];
    $http.get('/api/ReportAnIssue')
    .success(function (data) {
        var id = 0;
        for (id; id < data.length; id++) {
            $scope.flags.push(data[id]);
        }
    });
});

sampleApp.controller('ReportAnIssueController',
    function ($scope, $http, $routeParams, $location) {

        $scope.id = $routeParams.flagId;
        $scope.comment = '';
        $scope.screens = [{ Id: '0', Name: 'Please select' }];
        $scope.selectedScreen = $scope.screens[0];
        $scope.fields = [{ Id: '0', Name: 'Please select' }];
        $scope.selectedField = $scope.fields[0];
        $scope.agents = [{ Id: '0', UserName: 'Please select' }];
        $scope.selectedAgent = $scope.agents[0];
        $scope.returnedField = null;
        $scope.details2 = null;

        $scope.complete = function () {
            console.log($scope.agents);
            $("#tags").autocomplete({
                source: $scope.agents
            });
        };


        $http.get(httpUrl + '/api/screen')
        .success(function (data) {
            for (id = 0; id < data.length; id++) {
                $scope.screens.push(data[id]);
            }
        });


        $http.get(httpUrl + '/GetAgents')
        .success(function (data) {
            for (id = 0; id < data.length; id++) {
                $scope.agents.push(data[id]);
            }
        });

        $scope.screenChange = function () {
            $scope.fields = [{ Id: '0', Name: 'Please select' }];
            $scope.selectedField = $scope.fields[0];
            $http.get(httpUrl + '/GetFieldsbyScreen?screenId=' + $scope.selectedScreen.Id).
            success(function (data) {

                for (id = 0; id < data.length; id++) {
                    $scope.fields.push(data[id]);
                    if ($scope.returnedField != null && $scope.returnedField.Id == data[id].Id) {
                        $scope.selectedField = data[id];
                    }

                }
            });
        };

        if ($scope.id    != 0) {
            $http.get(httpUrl + '/GetReportedIssueById?id=' + $scope.id)
        .success(function (data) {

            $scope.comment = data.Comment;
            $scope.returnedField = data.Field;
            angular.forEach($scope.screens, function (value, key) {
                if (value.Id == data.Screen.Id) {

                    if ($scope.selectedScreen != value.Id) {
                        $scope.selectedScreen = value;
                        $scope.screenChange();
                    }
                }
            });
            angular.forEach($scope.agents, function (value, key) {
                if (value.Id == data.Agent.Id) {
                    $scope.selectedAgent = value;
                }
            });
        });
        }
        $scope.saveData = function () {
            var data = {
                "Id": $scope.id,
                "Field": {
                    "Id": $scope.selectedField.Id,
                    "Name": $scope.selectedField.Name
                },
                "Screen": {
                    "Id": $scope.selectedScreen.Id,
                    "Name": $scope.selectedScreen.Name
                },
                "Agent": {
                    "Id": $scope.selectedAgent.Id,
                    "UserName": $scope.selectedAgent.UserName
                },
                "Comment": $scope.comment
            };
            $http.post(httpUrl + '/Report', JSON.stringify(data),
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
            ).success(function (data) {
                alert("Saved succesfully.");
                $location.path('/ReportAMistake');
            });
        };

    });