package com.cacti.cactiphone.repository.data

class WebExtensions {

    suspend fun <T, R> grpcGet(
        call: suspend () -> R,
        map: (R) -> T,
    ) : Resource<T> {

        return try {
            val reply = call()
            val result = map(reply)
            Resource.success(result)

        } catch (e: Exception) {
            e.printStackTrace()
            var message = e.message ?: ""
            message += "\ncause:\n"
            message += e.cause?.message ?: ""
            Resource.error(message = message)
        }
    }

    suspend fun <T, R> grpcGetAll(
        call: suspend () -> List<R>,
        map: (R) -> T,
    ) : Resource<List<T>> {

        return try {
            val reply = call()

            val list = ArrayList<T>()
            for (grpc in reply) {
                val obj = map(grpc)
                list.add(obj)
            }

            Resource.success(list)

        } catch (e: Exception) {
            e.printStackTrace()
            var message = e.message ?: ""
            message += "\ncause:\n"
            message += e.cause?.message ?: ""
            Resource.error(message = message)
        }
    }

}