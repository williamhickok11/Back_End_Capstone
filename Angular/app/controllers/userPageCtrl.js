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
		$scope.allComments = [];
		console.log("selsectedUser", $scope.currSelectedUserId);

		// Get access to the user that has been clicked on
		$http
			.get(`http://localhost:49881/api/Musician/${$scope.currSelectedUserId}`)
			.success(selectedUser => {
				$scope.selectedUser = selectedUser[0];
				console.log($scope.selectedUser);
			})
		.then(function(){
			$http
			.get(`http://localhost:49881/api/Comments/${$scope.currSelectedUserId}`)
			.success(comments => {
				$scope.allComments = comments;
				console.log($scope.allComments);
			})
		})
	}
]);


