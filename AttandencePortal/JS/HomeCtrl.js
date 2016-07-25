myApp.controller(
			'HomeCtrl', ['$scope', 'crudApp', '$log', '$uibModal', function ($scope, crudApp, $log, $uibModal) {
			    $scope.time1 = new Date();
			    //$scope.Emp.IN = new Date();
			    //$scope.Emp.OUT = new Date();
			    $scope.time2 = new Date();
			    $scope.time2.setHours(09, 00);

			    $scope.showMeridian = false;
			    $scope.ShowAllEmp = true;
			    $scope.EditEmp = false;
			    $scope.disabled = false;

			    $scope.ShowHide = function () {
			        $scope.ShowAllEmp = !$scope.ShowAllEmp;
			        $scope.EditEmp = !$scope.EditEmp;
			    };
			    $scope.ShowHideCencel = function () {
			        $scope.ShowAllEmp = !$scope.ShowAllEmp;
			        $scope.EditEmp = !$scope.EditEmp;
			        crudApp.getAllEmployees().then(function (result) {
			            $scope.Emp = result;

			        });
			    };

			    crudApp.getAllEmployees().then(function (result) {
			        $scope.Emp = result;

			    });
			    crudApp.getEmployeesToday().then(function (result) {
			        $scope.EmpToday = result;

			    });
			    $scope.out = false;
			    $scope.showMe = false;
			    $scope.Show = function () {
			        $scope.showMe = true;
			    }
			    $scope.delEmpbyID = function (Aid) {

			        crudApp.delEmpbyID(Aid).then(function (result) {
			            $scope.Msg = result;
			            crudApp.getEmployeesToday().then(function (result) {
			                $scope.EmpToday = result;

			            });
			        });
			    };


			    $scope.EmployeeIN = function (Emp, e) {
			        crudApp.EmployeeIN(Emp, e).then(function (result) {
			            $scope.IN = false;
			            crudApp.getEmployeesToday().then(function (result) {
			                $scope.EmpToday = result;

			            });



			        });
			    };
			    $scope.UpdateAttandence = function (Emp) {
			        crudApp.UpdateAttandence(Emp).then(function (result) {
			            $scope.Msg = "Employee ID " + result + " has been updated successfully";
			            alert($scope.Msg);
			            $scope.ShowHide();
			            crudApp.getEmployeesToday().then(function (result) {
			                $scope.EmpToday = result;


			            });
			            crudApp.getAllEmployees().then(function (result) {
			                $scope.Emp = result;

			            });
			        });
			    };
			    $scope.EmployeeOUT = function (outtime, Eid) {
			        crudApp.EmployeeOUT(outtime, Eid).then(function (result) {
			            $scope.IN = false;
			            crudApp.getEmployeesToday().then(function (result) {
			                $scope.EmpToday = result;

			            });


			        });
			    };
			    $scope.getByAid = function (Aid) {
			        crudApp.getByAid(Aid).then(function (result) {
			            $scope.ShowAllEmp = false;
			            $scope.EditEmp = true;
			            result.IN = new Date(parseInt(result.IN.substr(6)));
			            result.OUT = new Date(parseInt(result.OUT.substr(6)));
			            $scope.Emp = result;
			        });
			    };
			    $scope.open = function (size) {

			        var modalInstance = $uibModal.open({
			            animation: $scope.animationsEnabled,
			            templateUrl: 'myModalContent.html',
			            controller: 'ModalInstanceCtrl',
			            size: size,
			            resolve: {
			                time: function () {
			                    return $scope.mytime;
			                }
			            }
			        });

			        modalInstance.result.then(function (selectedItem) {
			            $scope.mytime = selectedItem;
			        }, function () {
			            $log.info('Modal dismissed at: ' + new Date());
			        });
			    };
			    $scope.open2 = function (size) {

			        var modalInstance = $uibModal.open({
			            animation: $scope.animationsEnabled,
			            templateUrl: 'myModel2.html',
			            controller: 'ModalController2',
			            size: size,
			            resolve: {
			                time: function () {
			                    return $scope.mytime2;
			                }
			            }
			        });

			        modalInstance.result.then(function (selectedItem) {
			            $scope.mytime2 = selectedItem;
			        }, function () {
			            $log.info('Modal dismissed at: ' + new Date());
			        });
			    };

			    $scope.toggleAnimation = function () {
			        $scope.animationsEnabled = !$scope.animationsEnabled;
			    };


			}]);

// Please note that $uibModalInstance represents a modal window (instance) dependency.
// It is not the same as the $uibModal service used above.

myApp.controller('ModalInstanceCtrl',['$scope','$uibModalInstance','time', function ($scope, $uibModalInstance, time) {
    $scope.mytime = new Date();
    $scope.mytime2 = new Date();

    $scope.hstep = 1;
    $scope.mstep = 15;
    $scope.mytime = '';

    $scope.options = {
        hstep: [1, 2, 3],
        mstep: [1, 5, 10, 15, 25, 30]
    };
    $scope.ismeridian = true;
    $scope.mytime = time;
    $scope.selected = {
        mytime: $scope.mytime
    };

    $scope.toggleMode = function () {
        $scope.ismeridian = !$scope.ismeridian;
    };

    $scope.update = function () {
        var d = new Date();
        var d2 = new Date();
        d.setHours(18);
        d.setMinutes(0);
        $scope.mytime2 = d2;
        $scope.mytime = d;
    };

    $scope.changed = function () {
        console.log('Time changed to: ' + $scope.mytime);
    };

    $scope.clear = function () {
        $scope.mytime = null;
    };
    $scope.ok = function () {
        $uibModalInstance.close($scope.mytime);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

}]);