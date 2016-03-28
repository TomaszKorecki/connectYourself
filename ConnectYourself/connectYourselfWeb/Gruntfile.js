module.exports = function(grunt) {
  grunt.initConfig({
    watch: {
      files: ['Gruntfile.js', 'content/css/*.css', 'app/**/*.js', 'app/**/*.html'],
      options: {
                livereload: true
        }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.registerTask('default', [
    'watch'
    ]);
};