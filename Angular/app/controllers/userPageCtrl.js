"use strict";

SwapNShop.controller("userPageCtrl", [
  	"$scope",
  	"$http",
  	"$location",
  	"$route",
  	"$routeParams",
  	"EquipFactory",
  	"AuthFactory",
  	"SelectedUserFactory",

  	function ($scope, $http, $location, $route, $routeParams, EquipFactory, AuthFactory, SelectedUserFactory) {
  		$scope.currUser = AuthFactory.getUser();
      	$scope.currSelectedUserId = SelectedUserFactory.getUserId();
		$scope.selectedUser = {};
		$scope.allComments = [];
		$scope.newComment = {};
		console.log("selsectedUser", $scope.currSelectedUserId);

		// View the page of the user you clicked on
      	$scope.goToPerson = function (id) {
      		console.log(id);
	      	SelectedUserFactory.setUserId(id)
	      	if ($scope.currUser.IdMusician == id){
	        	$location.path("/profile");
	      	} else {
	        	$location.path("/user_page");
	      	}
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
			}).
			then(function(){
				$scope.newComment.message = "";
				$route.reload();
			});
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
				for (var i = 0; i < $scope.allComments.length; i++) {
						var date = $scope.allComments[i].date.split(/\-|\T/);
						$scope.allComments[i].date = date[1] + "/" + date[2] + "/" + date[0];
					}
				console.log($scope.allComments);
			})
		})
	}
]);


