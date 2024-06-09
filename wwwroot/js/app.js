var app = angular.module('app', []);

app.controller('UsuarioController', ['$scope', '$http', function($scope, $http) {
    $scope.usuario = {};

    $scope.salvarUsuario = function() {
        $http.post('/api/cadastrar', $scope.usuario)
            .then(function(response) {
                alert(response.data.Message);
            })
            .catch(function(error) {
                alert('Erro ao cadastrar usuário: ' + error.data.Message);
            });
    };
}]);