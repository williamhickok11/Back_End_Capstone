"use strict";

/* exported MusicHistory */

let SwapNShop = angular.module("SongApp", ["ngRoute"])

SwapNShop.config(["$routeProvider",
  function ($routeProvider) {
    $routeProvider.
      when("/", {
        templateUrl: "partials/homepage.html",
        controller: 'mainCtrl'
      }).
      // when("/create", {
      //   templateUrl: "partials/create.html",
      //   controller: 'createCtrl'
      // }).
      // when("/register", {
      //   templateUrl: "partials/register.html",
      //   controller: 'registerCtrl'
      // }).
      otherwise({
        redirectTo: "/"
      });
  }]);

