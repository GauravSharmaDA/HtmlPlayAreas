
sampleApp.controller('ScriptDetailController', function ($scope, $routeParams, $http,$location) {
    var id = 0;
    $scope.id = $routeParams.scriptId;
    $scope.title = '';
    $scope.screens = [{ Id: '0', Name: 'Please select' }];
    $scope.selectedScreen = $scope.screens[0];
    $scope.fields = [{ Id: '0', Name: 'Please select' }];
    $scope.selectedField = $scope.fields[0];
    $scope.languages = [{ Id: '0', Name: 'Please select' }]
    $scope.selectedLanguage = $scope.languages[0];
    $scope.products = [{ Id: '0', Name: 'Please select' }]
    $scope.selectedProduct = $scope.products[0];
    $scope.script = '';

    $http.get(httpUrl + '/api/screen')
    .success(function (data) {
        for (id = 0; id < data.length; id++) {
            $scope.screens.push(data[id]);
        }
    });

    $http.get(httpUrl + '/GetProducts')
    .success(function (data) {
        for (id = 0; id < data.length; id++) {
            $scope.products.push(data[id]);
        }
    });
    $http.get(httpUrl + '/GetLanguage')
       .success(function (data) {
           id = 0;
           for (id = 0; id < data.length; id++) {
               $scope.languages.push(data[id]);
           }
       });
    $scope.returnedField = null;
    $scope.screenChange = function () {
        $scope.fields = [{ Id: '0', Name: 'Please select' }];
        $scope.selectedField = $scope.fields[0];
        $http.get(httpUrl + '/GetFieldsbyScreen?screenId=' + $scope.selectedScreen.Id).
        success(function (data) {
            
            for (id = 0; id < data.length; id++) {
                $scope.fields.push(data[id]);
                if ($scope.returnedField!=null && $scope.returnedField.Id == data[id].Id) {
                    $scope.selectedField = data[id];
                }

            }
        });
    };

    if ($scope.id != 0) {
        $http.get(httpUrl + '/GetScriptById?scriptId=' + $scope.id)
        .success(function (data) {
            $scope.title = data.Title;
            angular.forEach($scope.products, function (value, key) {
                if (value.Id == data.Product.Id) {
                    $scope.selectedProduct = value;
                }
            });
            $scope.returnedField = data.Field;
            angular.forEach($scope.screens, function (value, key) {
                if (value.Id == data.Screen.Id) {

                    if ($scope.selectedScreen != value.Id) {
                        $scope.selectedScreen = value;
                        $scope.screenChange();
                    }
                }
            });
            angular.forEach($scope.languages, function (value, key) {
                if (value.Id == data.Language.Id) {
                    $scope.selectedLanguage = value;
                }
            });
            $scope.script = data.Text;
        }
        );
    }

    $scope.saveData = function () {
        var data =
            {
                "Id": $scope.id,
                "Title": $scope.title,
                "Language": {
                    "Id": $scope.selectedLanguage.Id,
                    "Name": $scope.selectedLanguage.Name
                },
                "Product": {
                    "Id": $scope.selectedProduct.Id,
                    "Name": $scope.selectedProduct.Name
                },
                
                "Screen": {
                    "Id": $scope.selectedScreen.Id,
                    "Name": $scope.selectedScreen.Name
                    
                },
                "Field": {
                    "Id": $scope.selectedField.Id,
                    "Name": $scope.selectedField.Name
                },
                "Text": $scope.script
            };
        $http.post(httpUrl + '/Save', JSON.stringify(data),
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
            ).success(function (data) {
                alert("Saved succesfully.");
                $location.path('/#ManageScripts');
            });
            
    };
});