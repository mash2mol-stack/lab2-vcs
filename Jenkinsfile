
pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                echo 'Код получен из GitHub'
                bat 'dir'
            }
        }

        stage('Build') {
            steps {
                echo 'Сборка проекта...'

                bat 'msbuild AuthenticationLab\\AuthenticationLab.csproj /t:Build /p:Configuration=Release'
            }
        }

        stage('Test') {
            steps {
                echo 'Запуск тестов...'

                bat 'dotnet test AuthenticationLab.Tests\\AuthenticationLab.Tests.csproj --configuration Release --logger "console;verbosity=normal"'
            }
        }
    }

    post {
        success {
            echo 'Пайплайн завершён успешно!'
        }

        failure {
            echo 'Пайплайн завершился с ошибкой'
        }
    }
}
