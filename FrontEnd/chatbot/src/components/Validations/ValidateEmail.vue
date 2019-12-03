<template>
<div>
    <div class="error" v-if="!v.form.Email.required">Введите поле</div>
    <div class="error" v-if="!v.form.Email.email">Введите email</div>
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