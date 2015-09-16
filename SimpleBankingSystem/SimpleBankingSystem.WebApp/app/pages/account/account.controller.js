angular.module('app')
    .controller('AccountCtrl', ['$scope', '$location', '$routeParams', 'AccountService', function ($scope, $location, $routeParams, accountService) {
        $scope.accountId = $routeParams.accountId;

        $scope.operations = [{
            name: 'Deposit',
            type: 1
        }, {
            name: 'Withdraw',
            type: 2
        }];

        $scope.selectedOperation = $scope.operations[0];

        $scope.execute = function () {
            accountService.executeTransaction({ accountId: $scope.accountId, type: $scope.selectedOperation.type, amount: $scope.amount }, function (result) {
                $scope.balance = result.data.balance;
                $scope.transactions.push(result.data.transaction);
            });
        };

        accountService.getAccountDetails({ accountId: $scope.accountId }, function (result) {
            if (result.isValid)
                $scope.balance = result.data.balance;
        });

        accountService.getTransactions({ accountId: $scope.accountId }, function (result) {
            if (result.isValid)
                $scope.transactions = result.data;
        });
    }]);