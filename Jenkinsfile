node {
    def mvnHome
    def commitId
    def packageN

    properties([gitLabConnection('GitLab')])
    
    stage('Preparation') { 
        cleanWs()

        checkout scm
        commitId = sh(returnStdout: true, script: 'git rev-parse HEAD')
        updateGitlabCommitStatus name: 'restore', state: 'pending', sha: commitId
        updateGitlabCommitStatus name: 'build', state: 'pending', sha: commitId
        updateGitlabCommitStatus name: 'publish', state: 'pending', sha: commitId
        updateGitlabCommitStatus name: 'test', state: 'pending', sha: commitId
        if(env.BRANCH_NAME == 'master'){
            updateGitlabCommitStatus name: 'pack', state: 'pending', sha: commitId
            updateGitlabCommitStatus name: 'deploy', state: 'pending', sha: commitId
        }
        updateGitlabCommitStatus name: 'clean', state: 'pending', sha: commitId
    }

    try{
        stage('Restore') {
            updateGitlabCommitStatus name: 'restore', state: 'running', sha: commitId 
            sh 'dotnet restore'
            updateGitlabCommitStatus name: 'restore', state: 'success', sha: commitId 
        }
    }catch(Exception ex){
        updateGitlabCommitStatus name: 'restore', state: 'failed', sha: commitId
        updateGitlabCommitStatus name: 'build', state: 'canceled', sha: commitId
        updateGitlabCommitStatus name: 'publish', state: 'canceled', sha: commitId
        updateGitlabCommitStatus name: 'test', state: 'canceled', sha: commitId
        if(env.BRANCH_NAME == 'master'){
            updateGitlabCommitStatus name: 'pack', state: 'canceled', sha: commitId
            updateGitlabCommitStatus name: 'deploy', state: 'canceled', sha: commitId
        }
        updateGitlabCommitStatus name: 'clean', state: 'canceled', sha: commitId
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return 
    }
    try{
        stage('Build'){
            updateGitlabCommitStatus name: 'build', state: 'running', sha: commitId 
            sh 'dotnet build'
            updateGitlabCommitStatus name: 'build', state: 'success', sha: commitId
        }
    }catch(Exception ex){
        updateGitlabCommitStatus name: 'build', state: 'failed', sha: commitId
        updateGitlabCommitStatus name: 'publish', state: 'canceled', sha: commitId
        updateGitlabCommitStatus name: 'test', state: 'canceled', sha: commitId
        if(env.BRANCH_NAME == 'master'){
            updateGitlabCommitStatus name: 'pack', state: 'canceled', sha: commitId
            updateGitlabCommitStatus name: 'deploy', state: 'canceled', sha: commitId
        }
        updateGitlabCommitStatus name: 'clean', state: 'canceled', sha: commitId
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }

    try{
        stage('Publish'){
            updateGitlabCommitStatus name: 'publish', state: 'running', sha: commitId
            //sh 'dotnet publish -c release'
            updateGitlabCommitStatus name: 'publish', state: 'success', sha: commitId
        }
    }catch(Exception ex){
        updateGitlabCommitStatus name: 'publish', state: 'failed', sha: commitId
        updateGitlabCommitStatus name: 'test', state: 'canceled', sha: commitId
        if(env.BRANCH_NAME == 'master'){
            updateGitlabCommitStatus name: 'pack', state: 'canceled', sha: commitId
            updateGitlabCommitStatus name: 'deploy', state: 'canceled', sha: commitId
        }
        updateGitlabCommitStatus name: 'clean', state: 'canceled', sha: commitId
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }

    try{
        stage('Tests') {
            gitlabCommitStatus("test") {
                sh 'dotnet test'
            }
        }
    }catch(Exception ex){
        updateGitlabCommitStatus name: 'test', state: 'failed', sha: commitId
        if(env.BRANCH_NAME == 'master'){
            updateGitlabCommitStatus name: 'pack', state: 'canceled', sha: commitId
            updateGitlabCommitStatus name: 'deploy', state: 'canceled', sha: commitId
        }
        updateGitlabCommitStatus name: 'clean', state: 'canceled', sha: commitId
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }

    try{
        if(env.BRANCH_NAME == 'master'){
            stage('NuGet'){
                mvnHome = env.BUILD_NUMBER
                packageN = "2.1.${mvnHome}"
                updateGitlabCommitStatus name: 'pack', state: 'running', sha: commitId
                println 'Directory:'
                sh 'ls'
                println 'Next Directory:'
                dir('App.IdentityProvider/'){
                    sh 'ls'
                    sh "dotnet build -c Release"
                    sh "dotnet pack -p:PackageVersion=${packageN} -c Release"
                    // sh "nuget push -src http://localhost:8083/ -ApiKey eCX22OBdshdncDSMF0DU /App.IdentityProvider/bin/Release/*.nupkg"
                    println 'After Pack:'
                    sh 'ls'
                }
                updateGitlabCommitStatus name: 'pack', state: 'success', sha: commitId
            }   
        }
    }catch(Exception ex){
        updateGitlabCommitStatus name: 'pack', state: 'failed', sha: commitId
        updateGitlabCommitStatus name: 'deploy', state: 'canceled', sha: commitId
        updateGitlabCommitStatus name: 'clean', state: 'canceled', sha: commitId
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }

    try{
        if(env.BRANCH_NAME == 'master'){
            stage('Deploy'){
                updateGitlabCommitStatus name: 'deploy', state: 'running', sha: commitId 
                dir('App.IdentityProvider/bin/Release/') {
                    sh "nuget push -src http://localhost:8083/ -ApiKey eCX22OBdshdncDSMF0DU ./*${packageN}.nupkg"
                    sh "ls"
                }
                updateGitlabCommitStatus name: 'deploy', state: 'success', sha: commitId
            }
        }
    }catch(Exception ex){
        updateGitlabCommitStatus name: 'deploy', state: 'failed', sha: commitId
        updateGitlabCommitStatus name: 'clean', state: 'canceled', sha: commitId
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }

    try{
        stage('Clean Up'){
        updateGitlabCommitStatus name: 'clean', state: 'running', sha: commitId
            cleanWs()
        updateGitlabCommitStatus name: 'clean', state: 'success', sha: commitId
        }
    }catch(Exception ex){
        updateGitlabCommitStatus name: 'clean', state: 'failed', sha: commitId
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }        
}
