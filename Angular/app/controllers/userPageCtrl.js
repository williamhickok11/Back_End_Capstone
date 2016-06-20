"use strict";

SwapNShop.controller("userPageCtrl", [
  	"$scope",
  	"$http",
  	"$location",
  	"$routeParams",
  	"EquipFactory",
  	"AuthFactory",
  	"SelectedUserFactory",

  	function ($scope, $http, $location, $routeParams, EquipFactory, AuthFactory, SelectedUserFactory) {
		$scope.currUser = AuthFactory.getUser();
      	$scope.currSelectedUserId = SelectedUserFactory.getUserId();
		$scope.selectedUser = {};
		console.log("currUser", $scope.currUser);
		console.log("selsectedUser", $scope.currSelectedUserId);

		if ($scope.currUser === null) {
			// Get access to the loged in user
			$http
				.get(`http://localhost:49881/api/Musician/${$scope.currSelectedUserId}`)
				.success(selectedUser => {
					$scope.selectedUser = selectedUser[0];
					console.log($scope.selectedUser);
				});
		} else {
			// Get access to the user that has been clicked on
			$http
				.get(`http://localhost:49881/api/Musician/${$scope.currSelectedUserId}`)
				.success(selectedUser => {
					$scope.selectedUser = selectedUser[0];
					console.log($scope.selectedUser);
				});
		}
	}
]);


