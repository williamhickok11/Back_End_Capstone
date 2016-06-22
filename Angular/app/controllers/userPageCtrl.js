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
		$scope.newComment = {};
		console.log("selsectedUser", $scope.currSelectedUserId);

		// View the page of the user you clicked on
	    $scope.goToPerson = function (id) {
	      	SelectedUserFactory.setUserId(id)
	      	$location.path("/user_page");
	    }

	    $scope.leaveAComment = function() {
	    	$scope.newComment.IdPostingMusician = $scope.currUser.IdMusician;
	    	$scope.newComment.date = new Date();
	    	$scope.newComment.IdRecievingMusician = SelectedUserFactory.getUserId();
	    	console.log("new comment", $scope.newComment);
	    	// post to the database
			$http({
				url:'http://localhost:49881/api/Comments',
				method: 'POST',
				data: JSON.stringify($scope.newComment)
			})
	    }
	    
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


