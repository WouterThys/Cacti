plugins {
    id("com.android.application")
    id("org.jetbrains.kotlin.android")
    id("kotlin-android")
    kotlin("kapt")
    id("kotlin-kapt")
    id("com.google.dagger.hilt.android")
    id("com.google.protobuf")
    id("androidx.navigation.safeargs")
}

android {
    namespace = "com.cacti.cactiphone"
    compileSdk = 35

    defaultConfig {
        applicationId = "com.cacti.cactiphone"
        minSdk = 30
        targetSdk = 34
        versionCode = 1
        versionName = "1.0"

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
    }

    signingConfigs {
        create("release") {
            storeFile = file("keys\\androidkey.jks")
            storePassword = "androidkey"
            keyAlias = "androidkey"
            keyPassword = "androidkey"
        }
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            isShrinkResources = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
            signingConfig = signingConfigs.getByName("release")
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_17
        targetCompatibility = JavaVersion.VERSION_17
    }
    kotlinOptions {
        jvmTarget = "17"
    }
    buildFeatures {
        dataBinding = true
        viewBinding = true
    }
}

protobuf {
    protoc { artifact =  "com.google.protobuf:protoc:3.24.1" }
    plugins {
        create("java") {
            artifact = "io.grpc:protoc-gen-grpc-java:1.57.2"
        }
        create("grpc") {
            artifact = "io.grpc:protoc-gen-grpc-java:1.57.2"
        }
        create("grpckt") {
            artifact = "io.grpc:protoc-gen-grpc-kotlin:1.3.1:jdk8@jar"
        }
    }
    generateProtoTasks {
        all().forEach {
            it.plugins {
                create("java") {
                    option("lite")
                }
                create("grpc") {
                    option("lite")
                }
                create("grpckt") {
                    option("lite")
                }
            }
            it.builtins {
                create("kotlin") {
                    option("lite")
                }
            }
        }
    }
}

dependencies {

    implementation("androidx.core:core-ktx:1.16.0")
    implementation("androidx.appcompat:appcompat:1.7.0")
    implementation("com.google.android.material:material:1.12.0")
    implementation("androidx.constraintlayout:constraintlayout:2.2.1")
    implementation("androidx.recyclerview:recyclerview:1.4.0")
    implementation("androidx.lifecycle:lifecycle-extensions:2.2.0")
    implementation("androidx.fragment:fragment-ktx:1.8.6")
    implementation("androidx.swiperefreshlayout:swiperefreshlayout:1.1.0")

    implementation("androidx.preference:preference-ktx:1.2.1")

    testImplementation("junit:junit:4.13.2")
    androidTestImplementation("androidx.test.ext:junit:1.1.5")
    androidTestImplementation("androidx.test.espresso:espresso-core:3.5.1")

    // Retrofit
    val retrofitVersion = "2.9.0"
    implementation("com.squareup.retrofit2:retrofit:$retrofitVersion")

    // OkHttp
    val okHttpVersion = "4.9.1"
//    implementation(platform("com.squareup.okhttp3:okhttp-bom:4.9.1"))
    implementation("com.squareup.okhttp3:okhttp:$okHttpVersion")
    implementation("com.squareup.okhttp3:logging-interceptor:$okHttpVersion")

    // Coroutines
    val jetbrainsCoroutineVersion = "1.7.3"
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:$jetbrainsCoroutineVersion")
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-android:$jetbrainsCoroutineVersion")

    //Lifecycle
    val androidxLifecycleVersion = "2.7.0"
    val androidxLifecycleExtensionsVersion = "2.2.0"
    implementation("androidx.lifecycle:lifecycle-viewmodel-ktx:$androidxLifecycleVersion")
    implementation("androidx.lifecycle:lifecycle-livedata-ktx:$androidxLifecycleVersion")
    //implementation("androidx.lifecycle:lifecycle-common-java8:$androidxLifecycleVersion")
    implementation("androidx.lifecycle:lifecycle-extensions:$androidxLifecycleExtensionsVersion")

    val glideVersion = "4.14.2"
    implementation("com.github.bumptech.glide:glide:$glideVersion")
    //implementation("com.github.bumptech.glide:okhttp3-integration:$glideVersion")
    kapt("com.github.bumptech.glide:compiler:$glideVersion") // This breaks the okhttp interceptor when using ksp

//    // Grpc
    val grpcVersion = "1.57.2"
    val grpcKotlinVersion = "1.3.1"
    val protobufVersion = "3.24.1"
    runtimeOnly("io.grpc:grpc-okhttp:$grpcVersion")
    implementation("io.grpc:grpc-okhttp:$grpcVersion")
    implementation("io.grpc:grpc-android:$grpcVersion")
    implementation("io.grpc:grpc-protobuf-lite:$grpcVersion")
    implementation("io.grpc:grpc-kotlin-stub:$grpcKotlinVersion")
    implementation("com.google.protobuf:protobuf-kotlin-lite:$protobufVersion")
//
    // Room
    val androidxRoomVersion = "2.7.1"
    implementation("androidx.room:room-runtime:$androidxRoomVersion")
    implementation("androidx.room:room-ktx:$androidxRoomVersion")
    annotationProcessor("androidx.room:room-compiler:$androidxRoomVersion")
    kapt("androidx.room:room-compiler:$androidxRoomVersion")

    //Hilt
    val daggerHiltVersion = "2.51.1"
    val androidxHiltVersion = "1.2.0"
    implementation("com.google.dagger:hilt-android:$daggerHiltVersion")
    kapt("com.google.dagger:hilt-android-compiler:$daggerHiltVersion")
    kapt("androidx.hilt:hilt-compiler:$androidxHiltVersion")


    // Navigation
    val navVersion = "2.7.7"
    implementation("androidx.navigation:navigation-fragment-ktx:$navVersion")
    implementation("androidx.navigation:navigation-ui-ktx:$navVersion")

    // Scanning
    val scanningVersion = "17.3.0"
    implementation("com.google.mlkit:barcode-scanning:$scanningVersion")
    implementation("com.google.android.gms:play-services-mlkit-barcode-scanning:18.3.0")

    val cameraxVersion = "1.4.2"
    implementation("androidx.camera:camera-core:${cameraxVersion}")
    implementation("androidx.camera:camera-camera2:${cameraxVersion}")
    implementation("androidx.camera:camera-lifecycle:${cameraxVersion}")
    implementation("androidx.camera:camera-view:${cameraxVersion}")
    implementation("androidx.camera:camera-extensions:${cameraxVersion}")
}

// Allow references to generated code
kapt {
    correctErrorTypes = true
}