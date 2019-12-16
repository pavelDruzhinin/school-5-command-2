<template>
    <div>
        <div class="error" v-if="!v.form.ConfirmPassword.sameAsPassword">
            Пароли должны совпадать
        </div>
    </div>
</template>

<script>
export default {
    props:{
        v:{
            type:Object,
            required:true
        }
    }
}
</script>