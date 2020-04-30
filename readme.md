# NetCore with Identity Server Sample Project

Oauth 2.x의 최소 표준 규약을 따르는 로그인 Flow 구성하고

인증에 필요한 모든 시작 서비스의, 템플릿 프로젝트로 활용

코드 자체를 오픈함으로, 비표준을 지양하고 코드퀄리티 향상

주의 : 설정화 기능이 없거나,하드코딩 될수도 있으며 중요한 스펙이 누락될수도 있음

## 이 프로젝트의 컨셉

- Docker 베이스 로컬에 올인원 작동가능 프로젝트로
- 인증 서버에 필요한 DB 스키마 자동생성
- 캐시서버 Redis 자동 구축
- 비표준으로 작성된 인증서버와 연동하기
- AS Developer 로서 ORM을 선호 하지만,DBA의 지혜가 중요함으로 SQL 모드로 작성 (DDL,DML 모두 형상관리요소)
- 기본적인 레파지토리 패턴 준수

# Resource Server와 Authorization Server 서버 분리목표

- 커다란 서비스는 인증 서버를 분리하거나 다중화 할 수 있어야 함.
- Authorization(Identity) Server 는 인증에 관련된 기능만 존재
- Resource(API서버)는 Auth 서버의 인증을 활용하고,클라이언트의 안전한 접근제공


# 표준 서버를 구축함으로 얻는 이득

- 표준인증 서버를 최초 구축하는것은 어려움이 있으나, 이후 인증이 필요한 마이크로서비스의 생성 가속화
- 외부 인증서버와 규약을 맞춤으로 연동에 용이 ( 소셜연동,SAML연동 등등)
- 로그인 보안이 높은경우 OKTA와 같은 MFA(기기추가인증) 보안 솔류션을 연동하기에도 용이해짐

# 도전 과제

- 신규 서비스에서 표준화 고려하는것은 약간의 노력으로 도입가능
- 진정한 고난은, 비표준적이고 보안 고려안된 내부 서비스의 권한롤을 고려하여 최소의 수정플랜으로 연동하는것

# Oauth 규격살펴보기

- Authorization Code Grant :
- Implicit Grant : +authorization code flow with the PKCE 웹용
- Resource Owner Password Credentials Grant : 
- Client Credentials Grant : 


# 참고 링크

이 프로젝트에 영향을 준 모든 참고 자료들

- http://docs.identityserver.io/en/3.1.0/intro/big_picture.html
- https://codebrains.io/how-to-add-jwt-authentication-to-asp-net-core-api-with-identityserver-4-part-1/ : API 인증방식(without UI)
- https://www.scottbrady91.com/OAuth/Why-the-Resource-Owner-Password-Credentials-Grant-Type-is-not-Authentication-nor-Suitable-for-Modern-Applications
- https://minwan1.github.io/2018/02/24/2018-02-24-OAuth/
- https://tansfil.tistory.com/60
- https://cheese10yun.github.io/spring-oauth2-jdbc/
- https://oauth.net/2/grant-types/implicit/
- https://velopert.com/2350 : 세션방식의 문제점
- https://jwt.io/ : JWT 토큰 분석기
- https://damienbod.com/2017/04/14/asp-net-core-identityserver4-resource-owner-password-flow-with-custom-userrepository/
- https://vmsdurano.com/apiboilerplate-and-identityserver4-access-control-for-apis/

