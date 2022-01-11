// generated on 2018-04-27 using generator-webapp 3.0.1
const gulp = require('gulp');
const gulpLoadPlugins = require('gulp-load-plugins');
const browserSync = require('browser-sync').create();
const del = require('del');
const wiredep = require('wiredep').stream;
const runSequence = require('run-sequence');
const $ = gulpLoadPlugins();
const reload = browserSync.reload;
let dev = true;
const path = require('path');
isMac = path.sep === '\\' ? false : true;
gulp.task('views', () => {
	return gulp.src('app/*.pug')
		.pipe($.plumber())
		.pipe($.pug({
			pretty: true
		}))
		.pipe(gulp.dest('.tmp'))
		.pipe(reload({
			stream: true
		}));
});
gulp.task('criticalCSS', () => {
	return gulp.src('app/styles/criticalCSS.scss')
		.pipe($.plumber())
		.pipe($.sass.sync({
			outputStyle: 'compressed',
			precision: 10,
			includePaths: ['.']
		}).on('error', $.sass.logError))
		.pipe($.autoprefixer({
			browsers: ['> 1%', 'last 2 versions', 'Firefox ESR']
		}))
		.pipe(gulp.dest('.tmp/styles'))
		.pipe(gulp.dest('dist/styles'));
});
gulp.task('styles', () => {
	return gulp.src('app/styles/*.scss')
		.pipe($.plumber())
		.pipe($.if(dev, $.sourcemaps.init()))
		.pipe($.sass.sync({
			outputStyle: 'expanded',
			precision: 10,
			includePaths: ['.']
		}).on('error', $.sass.logError))
		.pipe($.autoprefixer({
			browsers: ['> 1%', 'last 2 versions', 'Firefox ESR']
		}))
		.pipe($.if(dev, $.sourcemaps.write()))
		.pipe(gulp.dest('.tmp/styles'))
		.pipe(reload({
			stream: true
		}));
});
gulp.task('scripts', () => {
	return gulp.src('app/scripts/**/*.js')
		.pipe($.plumber())
		.pipe($.if(dev, $.sourcemaps.init()))
		.pipe($.babel())
		.pipe($.if(dev, $.sourcemaps.write('.')))
		.pipe(gulp.dest('.tmp/scripts'))
		.pipe(reload({
			stream: true
		}));
});

function lint(files) {
	return gulp.src(files)
		.pipe($.eslint({
			fix: true
		}))
		.pipe(reload({
			stream: true,
			once: true
		}))
		.pipe($.eslint.format())
		.pipe($.if(!browserSync.active, $.eslint.failAfterError()));
}
gulp.task('lint', () => {
	return lint('app/scripts/**/*.js')
		.pipe(gulp.dest('app/scripts'));
});
gulp.task('lint:test', () => {
	return lint('test/spec/**/*.js')
		.pipe(gulp.dest('test/spec'));
});
gulp.task('html', ['views', 'styles', 'scripts'], () => {
	// return gulp.src('app/*.html')
	return gulp.src(['app/*.html', '.tmp/*.html'])
		.pipe($.useref({
			searchPath: ['.tmp', 'app', '.']
		}))
		.pipe($.if(/\.js$/, $.uglify({
			compress: {
				drop_console: false
			}
		})))
		.pipe($.if(/\.css$/, $.cssnano({
			safe: true,
			autoprefixer: false
		})))
		.pipe($.if(/\.html$/, $.htmlmin({
			//   collapseWhitespace: true,
			minifyCSS: true,
			minifyJS: {
				compress: {
					drop_console: true
				}
			},
			processConditionalComments: true,
			removeComments: true,
			removeEmptyAttributes: true,
			removeScriptTypeAttributes: true,
			removeStyleLinkTypeAttributes: true
		})))
		.pipe(gulp.dest('dist'));
});
gulp.task('images', () => {
	return gulp.src('app/images/**/*')
		.pipe($.cache($.imagemin()))
		.pipe(gulp.dest('dist/images'));
});
gulp.task('fonts', () => {
	return gulp.src(require('main-bower-files')('**/*.{eot,svg,ttf,woff,woff2}', function (err) {})
			.concat('app/fonts/**/*'))
		.pipe($.if(dev, gulp.dest('.tmp/fonts'), gulp.dest('dist/fonts')));
});
gulp.task('extras', () => {
	return gulp.src([
    'app/*.*',
    '!app/*.html',
    '!app/*.pug',
    '!app/*.vscode'
  ], {
		dot: true
	}).pipe(gulp.dest('dist'));
});
gulp.task('clean', del.bind(null, ['.tmp', 'dist']));
gulp.task('serve', () => {
	runSequence(['wiredep'], 'criticalCSS', ['views', 'styles', 'scripts', 'fonts'], () => {
		browserSync.init({
			notify: false,
			port: 9000,
			server: {
				baseDir: ['.tmp', 'app'],
				routes: {
					'/bower_components': 'bower_components',
					'/node_modules': 'node_modules',
				}
			}
		});
		gulp.watch([
      'app/*.html',
      'app/images/**/*',
      '.tmp/fonts/**/*'
    ]).on('change', reload);
		gulp.watch(['app/**/*.pug', 'app/partials/**/*.pug', 'app/mixins/**/*.pug'], ['views']);
		gulp.watch(['app/styles/**/*.scss', 'app/partials/**/*.scss', 'app/mixins/**/*.scss'], ['criticalCSS', 'styles']);
		gulp.watch(['app/scripts/**/*.js', 'app/partials/**/*.js', 'app/mixins/**/*.js'], ['scripts']);
		gulp.watch('app/fonts/**/*', ['fonts']);
		gulp.watch('bower.json', ['wiredep', 'fonts']);
	});
});
gulp.task('serve:dist', ['default'], () => {
	browserSync.init({
		notify: false,
		port: 9000,
		server: {
			baseDir: ['dist']
		}
	});
});
gulp.task('serve:test', ['scripts'], () => {
	browserSync.init({
		notify: false,
		port: 9000,
		ui: false,
		server: {
			baseDir: 'test',
			routes: {
				'/scripts': '.tmp/scripts',
				'/bower_components': 'bower_components'
			}
		}
	});
	gulp.watch('app/scripts/**/*.js', ['scripts']);
	gulp.watch(['test/spec/**/*.js', 'test/index.html']).on('change', reload);
	gulp.watch('test/spec/**/*.js', ['lint:test']);
});
// inject bower components
gulp.task('wiredep', () => {
	gulp.src('app/styles/*.scss')
		.pipe($.filter(file => file.stat && file.stat.size))
		.pipe(wiredep({
			ignorePath: /^(\.\.\/)+/
		}))
		.pipe(gulp.dest('app/styles'));
	// gulp.src('app/*.html')
	gulp.src('app/layouts/*.pug')
		.pipe(wiredep({
			exclude: ['bootstrap'],
			ignorePath: /^(\.\.\/)*\.\./,
			fileTypes: {
				pug: {
					block: /(([ \t]*)\/\/-?\s*bower:*(\S*))(\n|\r|.)*?(\/\/-?\s*endbower)/gi,
					detect: {
						js: /script\(.*src=['"]([^'"]+)/gi,
						css: /link\(.*href=['"]([^'"]+)/gi
					},
					replace: {
						js: 'script(src=\'{{filePath}}\')',
						css: 'link(rel=\'stylesheet\', href=\'{{filePath}}\')'
					}
				}
			}
		}))
		// .pipe(gulp.dest('app'));
		.pipe(gulp.dest('app/layouts'));
});
gulp.task('build', ['lint', 'html', 'images', 'fonts', 'extras'], () => {
	return gulp.src('dist/**/*').pipe($.size({
		title: 'build',
		gzip: true
	}));
});
gulp.task('default', () => {
	return new Promise(resolve => {
		dev = false;
		runSequence(['clean', 'wiredep'], 'criticalCSS', 'build', resolve);
	});
});
var serverUrlFe = '//192.168.5.8/project/rabbit_today/fe';
var serverUrlBe = isMac ? '/Volumes/project/rabbit_today/be/Website/Assets/RabbitToday' : '//192.168.5.8/project/rabbit_today/be/Website/Assets/RabbitToday';
gulp.task('deployFe', () => {
	return gulp.src([
    'dist/**/*.*',
    '!dist/*.vscode',
    '!dist/robots.txt'
  ]).pipe(gulp.dest(serverUrlFe));
});
gulp.task('deploy', () => {
	return new Promise(resolve => {
		runSequence('deployCss', 'deployJs', 'deployIcons', resolve);
	});
});
gulp.task('deployFull', () => {
	return new Promise(resolve => {
		runSequence('deployCss', 'deployJs', 'deployFont', 'deployImages', resolve);
	});
});
gulp.task('deployCss', () => {
	return gulp.src([
    'dist/styles/*.css'
  ]).pipe(gulp.dest(serverUrlBe + '/styles'));
});
gulp.task('deployJs', () => {
	return gulp.src([
    'dist/scripts/**/*.js'
  ]).pipe(gulp.dest(serverUrlBe + '/js'));
});
gulp.task('deployFont', () => {
	return gulp.src([
    'dist/fonts/**/*.*'
  ]).pipe(gulp.dest(serverUrlBe + '/fonts'));
});
gulp.task('deployImages', () => {
	return gulp.src([
    'dist/images/**/*.*'
  ]).pipe(gulp.dest(serverUrlBe + '/images'));
});
gulp.task('deployIcons', () => {
	return gulp.src([
    'dist/images/icons/**/*.*'
  ]).pipe(gulp.dest(serverUrlBe + '/images/icons'));
});
